import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-emp-add-edit',
  templateUrl: './product-add-edit.component.html',
  styleUrls: ['./product-add-edit.component.css'],
})
export class ProductAddEditComponent implements OnInit {
  productForm: FormGroup; 

  constructor(
    private productService: ProductService,
    private dialogRef: MatDialogRef<ProductAddEditComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.productForm = this.formBuilder.group({
      name: ['', Validators.required],
      code: ['', Validators.required],
      price: ['', Validators.required]      
    });
  }

  ngOnInit(): void {
    this.productForm.patchValue(this.data);
  }

  onSubmit() {
    if (this.productForm.valid) {
      if (!this.data) {      
        this.productService.addEmployee(this.productForm.value).subscribe({
          next: (val: any) => {
            alert('Product added successfully!');
            this.productForm.reset();
            this.dialogRef.close(true);
          },
          error: (err: any) => {
            console.error(err);
            alert("Error while adding the product!");
          },
        });
      }
    }
  }
}