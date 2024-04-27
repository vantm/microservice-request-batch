import { CommonModule } from '@angular/common';
import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { Warehouse } from '../../features/warehouse/models/warehouse';
import { WarehouseService } from '../../features/warehouse/services/warehouse.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-warehouse-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './warehouse-list.component.html',
  styleUrl: './warehouse-list.component.scss',
})
export class WarehouseListComponent implements OnInit {
  warehouses: Warehouse[] = [];

  private readonly destroyRef = inject(DestroyRef);

  constructor(private readonly warehouseService: WarehouseService) {}

  ngOnInit(): void {
    this.warehouseService
      .list()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((warehouses) => {
        this.warehouses = warehouses;
      });
  }
}
