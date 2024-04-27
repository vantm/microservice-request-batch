import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { Product } from '../../features/product/models/product';
import { ProductService } from '../../features/product/services/product.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  private readonly destroyRef = inject(DestroyRef);

  constructor(private readonly productService: ProductService) {}

  ngOnInit(): void {
    this.productService
      .list()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((products) => {
        this.products = products;
      });
  }
}
