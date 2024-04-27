import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddWarehouse, Warehouse } from '../models/warehouse';
import { Observable, catchError } from 'rxjs';
import { Environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class WarehouseService {
  constructor(private readonly http: HttpClient) {}

  list(): Observable<Warehouse[]> {
    return this.http
      .get<Warehouse[]>(`${Environment.WarehouseApi}/warehouses`)
      .pipe(
        catchError((e, c) => {
          console.error(
            'An error was occurred when getting batches. Error: %o',
            e
          );
          return c;
        })
      );
  }

  add(data: AddWarehouse): Observable<Warehouse> {
    return this.http
      .post<Warehouse>(`${Environment.WarehouseApi}/warehouses`, data)
      .pipe(
        catchError((e, c) => {
          console.error(
            'An error was occurred when creating a warehouse. Error: %o',
            e
          );
          return c;
        })
      );
  }
}
