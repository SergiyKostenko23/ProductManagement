import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef
} from '@angular/material/dialog';

import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { QuantityDialogData } from '../../models/quantity-dialog-data';

@Component({
  selector: 'app-quantity-dialog',
  standalone: true,
  imports: [
    FormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './quantity-dialog.html',
  styleUrl: './quantity-dialog.scss'
})
export class QuantityDialog {

  readonly data = inject<QuantityDialogData>(MAT_DIALOG_DATA);

  private readonly dialogRef = inject(MatDialogRef<QuantityDialog>);

  quantity = 1;

  confirm(): void {

    if (this.quantity <= 0) {
      return;
    }

    this.dialogRef.close(this.quantity);

  }

  cancel(): void {

    this.dialogRef.close();

  }

}