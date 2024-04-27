import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddBatch, Batch } from '../models/batch';
import { Observable, catchError } from 'rxjs';
import { Environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BatchService {
  constructor(private readonly http: HttpClient) {}

  list(): Observable<Batch[]> {
    return this.http.get<Batch[]>('http://localhost:5067/batches').pipe(
      catchError((e, c) => {
        console.error(
          'An error was occurred when getting batches. Error: %o',
          e
        );
        return c;
      })
    );
  }

  add(data: AddBatch): Observable<Batch> {
    return this.http.post<Batch>(`${Environment.BatchApi}/batches`, data).pipe(
      catchError((e, c) => {
        console.error(
          'An error was occurred when creating a batch. Error: %o',
          e
        );
        return c;
      })
    );
  }
}
