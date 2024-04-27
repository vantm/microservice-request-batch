import { HttpClient } from '@angular/common/http';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { DestroyRef, Injectable, inject } from '@angular/core';
import {
  Observable,
  Subject,
  buffer,
  catchError,
  exhaustMap,
  mergeMap,
  tap,
  timer,
} from 'rxjs';

export interface BatchRequest {
  path: string;
  requestId: string;
}

export type BatchResponse =
  | {
      succeed: false;
      requestId: string;
      message: string;
    }
  | {
      succeed: true;
      requestId: string;
      result: object;
    };

export type BatchReply = {
  responses: BatchResponse[];
};

export interface BatchApi {
  remove(): void;
  queueBatch(path: string): void;
  result$: Observable<BatchResponse>;
}

@Injectable({
  providedIn: 'root',
})
export class BatchService {
  private path$ = new Subject<BatchRequest>();
  private res$: Observable<BatchResponse>;
  private subscribers: Record<string, Subject<BatchResponse>> = {};
  private readonly destroyRef = inject(DestroyRef);

  constructor(http: HttpClient) {
    this.res$ = this.path$.pipe(
      buffer(this.path$.pipe(exhaustMap(() => timer(100)))),
      mergeMap((x) =>
        http.post<BatchReply>('http://localhost:5188/batch', { requests: x })
      ),
      catchError((err, caught) => {
        console.log('Error: %o', err);
        return caught;
      }),
      mergeMap((reply) => reply.responses),
      tap((r) => {
        this.subscribers[r.requestId]?.next(r);
      }),
      takeUntilDestroyed(this.destroyRef)
    );

    this.res$.subscribe();
  }

  addApi(): BatchApi {
    const apiPath$ = new Subject<string>();
    const result$ = new Subject<BatchResponse>();
    const requestId = crypto.randomUUID();

    const apiSub = apiPath$
      .pipe(
        tap((p) => {
          this.path$.next({
            path: p,
            requestId,
          });
        })
      )
      .subscribe();

    this.subscribers[requestId] = result$;

    return {
      remove: () => {
        apiSub.unsubscribe();
        delete this.subscribers[requestId];
      },
      queueBatch(path) {
        apiPath$.next(path);
      },
      result$: result$,
    };
  }
}
