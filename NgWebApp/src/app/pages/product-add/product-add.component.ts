import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ProductService } from '../../features/product/services/product.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-product-add',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './product-add.component.html',
  styleUrl: './product-add.component.scss',
})
export class ProductAddComponent implements OnInit {
  productForm: FormGroup = null!;
  message: string | null = null;

  private readonly destroyRef = inject(DestroyRef);

  constructor(private readonly productService: ProductService) {}

  ngOnInit(): void {
    this.productForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(200),
      ]),
      price: new FormControl(0, [
        Validators.required,
        Validators.min(0.001),
        Validators.max(1000),
      ]),
    });
  }

  get name() {
    return this.productForm.get('name');
  }

  get price() {
    return this.productForm.get('price');
  }

  onAdd() {
    this.productForm.markAllAsTouched();

    if (this.productForm.valid) {
      this.productForm.markAsPending();

      this.productService
        .add(this.productForm.value)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe((product) => {
          this.productForm.reset();

          this.message = `The product ${product.name} had been added`;

          console.log('The product had been added: %o', product);

          setTimeout(() => {
            this.message = null;
          }, 3000);
        });
    }
  }
}
