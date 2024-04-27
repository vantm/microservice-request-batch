import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { WarehouseHeaderComponent } from '../../core/components/warehouse-header/warehouse-header.component';

@Component({
  selector: 'app-warehouse-home',
  standalone: true,
  imports: [RouterOutlet, WarehouseHeaderComponent],
  templateUrl: './warehouse-home.component.html',
  styleUrl: './warehouse-home.component.scss',
})
export class WarehouseHomeComponent {}
