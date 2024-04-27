import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarehouseService } from './services/warehouse.service';
import { BatchService } from './services/batch.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [CommonModule, HttpClientModule],
  providers: [WarehouseService, BatchService],
})
export class WarehouseModule {}
