import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { Dashboard } from '../models/dashboard';
import { LowStockProduct } from '../models/low-stock-product';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    `${environment.apiUrl}/dashboard`;

  getDashboard(): Observable<Dashboard> {

    return this.http.get<Dashboard>(this.apiUrl);

  }

  getLowStockProducts(): Observable<LowStockProduct[]> {

    return this.http.get<LowStockProduct[]>(
        `${this.apiUrl}/low-stock`
    );

  }

}