import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BatchService } from './batch.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [CommonModule, HttpClientModule],
  providers: [BatchService],
})
export class ServicesModule {}
