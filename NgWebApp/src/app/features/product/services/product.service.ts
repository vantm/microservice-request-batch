import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { AddProduct, Product } from '../models/product';
import { Environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private readonly http: HttpClient) {}

  list(): Observable<Product[]> {
    return this.http.get<Product[]>(`${Environment.BaseUrl}/products`).pipe(
      catchError((e, c) => {
        console.error(
          'An error was occurred when getting products. Error: %o',
          e
        );
        return c;
      })
    );
  }

  add(data: AddProduct): Observable<Product> {
    return this.http
      .post<Product>(`${Environment.BaseUrl}/products`, data)
      .pipe(
        catchError((e, c) => {
          console.error(
            'An error was occurred when creating a product. Error: %o',
            e
          );
          return c;
        })
      );
  }
}
