import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

import { ProductForm } from '../../components/product-form/product-form';
import { ProductService } from '../../services/product.service';
import { ProductFormValue } from '../../models/product-form-value';
import { CreateProduct } from '../../models/create-product';

@Component({
  selector:    'app-product-create',
  standalone:  true,
  imports:     [ProductForm],
  templateUrl: './product-create.html'
})
export class ProductCreate {

  private readonly productService = inject(ProductService);
  private readonly router         = inject(Router);

  create(formValue: ProductFormValue): void {

    const product: CreateProduct = {
      name:        formValue.name,
      description: formValue.description,
      category:    formValue.category,
      price:       formValue.price,
      stock:       formValue.stock
    };

    this.productService.create(product).subscribe({
      next: () => this.router.navigate(['/']),
      error: err => console.error(err)
    });

  }

}