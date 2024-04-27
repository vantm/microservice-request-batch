import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, concatMap } from 'rxjs/operators';
import { EMPTY, of } from 'rxjs';
import { CoreActions } from './core.actions';

@Injectable()
export class CoreEffects {
  loadCores$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(CoreActions.loadCores),
      concatMap(() =>
        /** An EMPTY observable only emits completion. Replace with your own observable API request */
        EMPTY.pipe(
          map((data) => CoreActions.loadCoresSuccess({ data })),
          catchError((error) => of(CoreActions.loadCoresFailure({ error })))
        )
      )
    );
  });

  constructor(private actions$: Actions) {}
}
