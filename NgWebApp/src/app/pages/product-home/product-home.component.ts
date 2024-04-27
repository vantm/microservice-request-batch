import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from '../../core/components/product-header/product-header.component';

@Component({
  selector: 'app-product-home',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './product-home.component.html',
  styleUrl: './product-home.component.scss',
})
export class ProductHomeComponent {}
