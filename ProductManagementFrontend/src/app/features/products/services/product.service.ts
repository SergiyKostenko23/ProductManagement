import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

import { Product } from '../models/product';
import { CreateProduct } from '../models/create-product';
import { UpdateProduct } from '../models/update-product';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private readonly http     = inject(HttpClient);
  private apiUrl             = `${environment.apiUrl}/products`;

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  create(product: CreateProduct): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  update(id: number, product: UpdateProduct): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/${id}`, product);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  addStock(id: number, quantity: number): Observable<Product> {
    return this.http.post<Product>(
      `${this.apiUrl}/${id}/add-to-stock/${quantity}`,{});
  }

  removeStock(id: number, quantity: number): Observable<Product> {
  return this.http.post<Product>(
    `${this.apiUrl}/${id}/decrement-stock/${quantity}`,{});
  }

  searchByName(name: string) {
    return this.http.get<Product[]>(
      `${this.apiUrl}/search?name=${name}`
    );
  }

getByStockRange(min: number, max: number) {
    return this.http.get<Product[]>(
      `${this.apiUrl}/stock-level?min=${min}&max=${max}`
    );
  }
}