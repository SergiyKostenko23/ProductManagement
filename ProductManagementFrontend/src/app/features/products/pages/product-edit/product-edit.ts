import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

import { ProductForm } from '../../components/product-form/product-form';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product';
import { ProductFormValue } from '../../models/product-form-value';
import { UpdateProduct } from '../../models/update-product';

@Component({
  selector:   'app-product-edit',
  standalone: true,
  imports: [
    CommonModule,
    ProductForm
  ],
  templateUrl: './product-edit.html',
  styleUrl:    './product-edit.scss'
})
export class ProductEdit implements OnInit {

  private readonly route          = inject(ActivatedRoute);
  private readonly router         = inject(Router);
  private readonly productService = inject(ProductService);

  product?: Product;

  ngOnInit(): void {

    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.productService.getById(id).subscribe({

      next: product => this.product = product,

      error: err => console.error(err)

    });

  }

  update(formValue: ProductFormValue): void {

    if (!this.product) {
      return;
    }

    const product: UpdateProduct = {

      id:          this.product.id,
      name:        formValue.name,
      description: formValue.description,
      category:    formValue.category,
      price:       formValue.price,
      stock:       formValue.stock

    };

    this.productService.update(product.id, product).subscribe({

      next: () => this.router.navigate(['/products', product.id]),

      error: err => console.error(err)

    });

  }

}