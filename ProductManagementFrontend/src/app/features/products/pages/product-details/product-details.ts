import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';

import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { ConfirmDialog } from '../../../../shared/components/confirm-dialog/confirm-dialog';
import { QuantityDialog } from '../../../../shared/components/quantity-dialog/quantity-dialog';

@Component({
  selector:    'app-product-details',
  standalone:  true,
  imports: [
  CommonModule,
  RouterLink,
  MatCardModule,
  MatButtonModule,
  MatIconModule,
  MatDividerModule
],
  templateUrl: './product-details.html',
  styleUrl:    './product-details.scss'
})
export class ProductDetails implements OnInit {

  private readonly route             = inject(ActivatedRoute);
  private readonly productService    = inject(ProductService);
  private readonly dialog            = inject(MatDialog);
  private readonly snackBar          = inject(MatSnackBar);
  private readonly router            = inject(Router);
  private readonly lowStockThreshold = 10;

  product?: Product;

  ngOnInit(): void {

    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.productService.getById(id).subscribe(product => {
      this.product = product;
    });

  }

  delete(): void {

    if (!this.product) {
      return;
    }

    const dialogRef = this.dialog.open(ConfirmDialog, {
      data: {
        title:       'Delete Product',
        message:     `Are you sure you want to delete "${this.product.name}"?`,
        confirmText: 'Delete',
        cancelText:  'Cancel'
      }
    });

    dialogRef.afterClosed().subscribe(result => {

      if (!result || !this.product) {
        return;
      }

      this.productService.delete(this.product.id).subscribe({

        next: () => {

          this.snackBar.open(
            'Product deleted successfully.',
            'Close',
            { duration: 3000 }
          );

          this.router.navigate(['/']);

        },

        error: err => {

          console.error(err);

          this.snackBar.open(
            'Unable to delete product.',
            'Close',
            { duration: 3000 }
          );

        }

      });

    });

  }

  addStock(): void {

    if (!this.product) {
      return;
    }

    const dialogRef = this.dialog.open(QuantityDialog, {

      data: {

        title:       'Add Stock',
        label:       'Quantity',
        confirmText: 'Add'

      }

    });

    dialogRef.afterClosed().subscribe(quantity => {

      if (!quantity) {
        return;
      }

      this.productService
        .addStock(this.product!.id, quantity)
        .subscribe({

          next: product => {

            this.product = product;

            this.snackBar.open(
              'Stock added successfully.',
              'Close',
              { duration: 3000 }
            );

          },

          error: err => console.error(err)

        });

    });

  }

  removeStock(): void {

    if (!this.product) {
      return;
    }

    const dialogRef = this.dialog.open(QuantityDialog, {

      data: {

        title:       'Remove Stock',
        label:       'Quantity',
        confirmText: 'Remove'

      }

    });

    dialogRef.afterClosed().subscribe(quantity => {

      if (!quantity) {
        return;
      }

      this.productService
        .removeStock(this.product!.id, quantity)
        .subscribe({

          next: product => {

            this.product = product;

            this.snackBar.open(
              'Stock removed successfully.',
              'Close',
              { duration: 3000 }
            );

          },

          error: err => {

            console.error(err);

            const message = err.error ?? 'Unable to remove stock.';

            this.snackBar.open(
              message,
              'Close',
              { duration: 3000 }
            );

          }

        });

    });

  }

  getStockStatus(): string {

  if (!this.product) {
    return '';
  }

  if (this.product.stock === 0) {
    return 'Out of Stock';
  }

  if (this.product.stock < this.lowStockThreshold) {
    return 'Low Stock';
  }

  return 'In Stock';

}

getStockClass(): string {

  if (!this.product) {
    return '';
  }

  if (this.product.stock === 0) {
    return 'out-of-stock';
  }

  if (this.product.stock < this.lowStockThreshold) {
    return 'low-stock';
  }

  return 'in-stock';

}

}