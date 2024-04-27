import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-warehouse-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './warehouse-header.component.html',
  styleUrl: './warehouse-header.component.scss',
})
export class WarehouseHeaderComponent {}
