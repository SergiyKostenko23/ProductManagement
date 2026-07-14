import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { RouterLink } from '@angular/router';

import { Dashboard } from '../../models/dashboard';
import { DashboardService } from '../../services/dashboard';
import { LowStockProduct } from '../../models/low-stock-product';
import { QuantityDialog } from '../../../../shared/components/quantity-dialog/quantity-dialog';
import { ProductService } from '../../../products/services/product.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatTableModule,
    MatButtonModule,
    RouterLink
  ],
  templateUrl: './dashboard.html',
  styleUrl:    './dashboard.scss'
})
export class DashboardComponent implements OnInit {

  private readonly dashboardService = inject(DashboardService);
  private readonly dialog           = inject(MatDialog);
  private readonly productService   = inject(ProductService);

  dashboard?:       Dashboard;
  lowStockProducts: LowStockProduct[] = [];  
  displayedColumns = [
    'productNumber',
    'name',
    'category',
    'stock',
    'actions'
  ];

  ngOnInit(): void {

    this.loadDashboard();

  }

  loadDashboard(): void {

    this.dashboardService
        .getDashboard()
        .subscribe({

          next: dashboard => {

            this.dashboard = dashboard;

          }

        });

    this.dashboardService
        .getLowStockProducts()
        .subscribe({

          next: products => {

            this.lowStockProducts = products;

          }

        });

  }

  addStock(product: LowStockProduct): void {

    const dialogRef = this.openQuantityDialog(
      'Add Stock',
      'Add'
    );

    dialogRef.afterClosed().subscribe(quantity => {

      if (!quantity) {

        return;

      }

      this.productService
          .addStock(product.id, quantity)
          .subscribe({

            next: () => {

              this.loadDashboard();

            }

          });

    });

  }

  private openQuantityDialog(title: string, confirmText: string) 
  {
    return this.dialog.open(QuantityDialog, {
      width: '400px',
      data: {
        title,
        label: 'Quantity',
        confirmText
      }
    });
  }

  getStockClass(product: LowStockProduct): string {

    if (product.stock <= 5) {
      return 'stock-low';
    }

    if (product.stock <= 10) {
      return 'stock-medium';
    }

    return 'stock-good';

  }

  getStockStatus(product: LowStockProduct): string {

    if (product.stock <= 5) {
      return 'Critical';
    }

    if (product.stock <= 10) {
      return 'Low';
    }

    return 'Healthy';

  }

}