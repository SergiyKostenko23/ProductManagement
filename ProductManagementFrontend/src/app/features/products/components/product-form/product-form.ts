import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { ProductFormValue } from '../../models/product-form-value';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './product-form.html',
  styleUrl:    './product-form.scss'
})
export class ProductForm implements OnChanges {

  private readonly fb = inject(FormBuilder);

  @Input()
  product?: ProductFormValue;

  @Input()
  buttonText = 'Save';

  @Output()
  submitted = new EventEmitter<ProductFormValue>();

  form = this.fb.nonNullable.group({
    name:          ['', Validators.required],
    description:   [''],
    category:      ['', Validators.required],
    price:         [0, [Validators.required, Validators.min(0)]],
    stock:         [0, [Validators.required, Validators.min(0)]]
  });

  ngOnChanges(changes: SimpleChanges): void {

    if (changes['product'] && this.product) {

      this.form.patchValue(this.product);

    }

  }

  onSubmit(): void {

    if (this.form.invalid) {

      this.form.markAllAsTouched();
      return;

    }

    this.submitted.emit(this.form.getRawValue());

  }

}