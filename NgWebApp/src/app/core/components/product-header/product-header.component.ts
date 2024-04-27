import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-product-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './product-header.component.html',
  styleUrl: './product-header.component.scss',
})
export class HeaderComponent {}
