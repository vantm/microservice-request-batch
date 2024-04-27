import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { AddAddress, Address } from '../models/address';
import { HttpClient } from '@angular/common/http';
import { Environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AddressService {
  constructor(private readonly http: HttpClient) {}

  list(): Observable<Address[]> {
    return this.http.get<Address[]>(`${Environment.AddressApi}/addresses`).pipe(
      catchError((e, c) => {
        console.error(
          'An error had occurred when getting addresses. Error: %o',
          e
        );
        return c;
      })
    );
  }

  add(data: AddAddress) {
    return this.http
      .post<Address>(`${Environment.AddressApi}/addresses`, data)
      .pipe(
        catchError((e, c) => {
          console.error(
            'An error was occurred when creating an address. Error: %o',
            e
          );
          return c;
        })
      );
  }
}
