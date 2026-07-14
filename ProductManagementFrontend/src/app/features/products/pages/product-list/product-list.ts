import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatChipsModule } from '@angular/material/chips';
import { FormsModule } from '@angular/forms';

import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { ConfirmDialog } from '../../../../shared/components/confirm-dialog/confirm-dialog';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';

@Component({
  selector:    'app-product-list',
  standalone:  true,
  imports: [
  CommonModule,
  RouterLink,
  MatTableModule,
  MatButtonModule,
  MatIconModule,
  MatFormFieldModule,
  MatInputModule,
  MatSortModule,
  MatPaginatorModule,
  MatChipsModule,
  FormsModule
],
  templateUrl: './product-list.html',
  styleUrl:    './product-list.scss'
})
export class ProductList implements OnInit, AfterViewInit {

  @ViewChild(MatSort)
  sort!: MatSort;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  private readonly productService = inject(ProductService);
  private readonly dialog         = inject(MatDialog);
  private readonly snackBar       = inject(MatSnackBar);
  searchText                      = '';
  minStock: number | null         = null;
  maxStock: number | null         = null;
  private readonly searchSubject  = new Subject<string>();
private readonly filterChanged    = new Subject<void>();

  dataSource = new MatTableDataSource<Product>();

    displayedColumns: string[] = [
    'productNumber',
    'name',
    'category',
    'price',
    'stock',
    'actions'
  ];

  ngAfterViewInit(): void {

    this.dataSource.sort = this.sort;

    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {

    console.log('Loading products...');

    this.loadProducts();

    this.searchSubject
      .pipe(
        debounceTime(300),
        distinctUntilChanged()
      )
      .subscribe(value => {

        if (!value.trim()) {

          this.loadProducts();
          return;

        }

        this.productService
          .searchByName(value)
          .subscribe(products => {

            this.dataSource.data = products;

          });

      });

      this.filterChanged
        .pipe(
          debounceTime(300)
        )
        .subscribe(() => {

          this.applyFilters();

        });
  }

  applyFilters(): void {

    const hasName =
      this.searchText.trim().length > 0;

    const hasStock =
      this.minStock != null &&
      this.maxStock != null;

    if (hasName) {

      this.productService
        .searchByName(this.searchText)
        .subscribe(products => {

          this.dataSource.data = products;

        });

      return;

    }

    if (hasStock) {

      this.productService
        .getByStockRange(
          this.minStock!,
          this.maxStock!
        )
        .subscribe(products => {

          this.dataSource.data = products;

        });

      return;

    }

    this.loadProducts();

  }

  resetFilters(): void {

    this.searchText = '';
    this.minStock   = null;
    this.maxStock   = null;
    this.loadProducts();

}

  loadProducts(): void {

    this.productService.getAll().subscribe({

      next: products => this.dataSource.data = products,

      error: err => console.error(err)

    });

  }

  delete(product: Product): void {

    const dialogRef = this.dialog.open(ConfirmDialog, {
      data: {
        title:       'Delete Product',
        message:     `Are you sure you want to delete "${product.name}"?`,
        confirmText: 'Delete',
        cancelText:  'Cancel'
      }
    });

    dialogRef.afterClosed().subscribe(result => {

      if (!result) {
        return;
      }

      this.productService.delete(product.id).subscribe({

        next: () => {

          this.snackBar.open(
            'Product deleted successfully.',
            'Close',
            { duration: 3000 }
          );

          this.loadProducts();

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

  onFiltersChanged(): void {

    this.filterChanged.next();

  }

  getStockClass(product: Product): string {

  if (product.stock === 0) {
    return 'out-of-stock';
  }

  if (product.stock < 10) {
    return 'low-stock';
  }

  return 'in-stock';

}

  getStockStatus(product: Product): string {

    if (product.stock === 0) {
      return 'Out of Stock';
    }

    if (product.stock < 10) {
      return 'Low Stock';
    }

    return 'In Stock';

  }

  searchStockRange() {

    if (
      this.minStock == null ||
      this.maxStock == null
    ) {

      this.loadProducts();
      return;

    }

    this.productService
      .getByStockRange(
        this.minStock,
        this.maxStock
      )
      .subscribe(products => {

        this.dataSource.data = products;

      });

  }
  
}