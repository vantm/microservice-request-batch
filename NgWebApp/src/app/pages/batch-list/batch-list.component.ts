import { CommonModule } from '@angular/common';
import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { Batch } from '../../features/warehouse/models/batch';
import { BatchService } from '../../features/warehouse/services/batch.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-batch-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './batch-list.component.html',
  styleUrl: './batch-list.component.scss',
})
export class BatchListComponent implements OnInit {
  batches: Batch[] = [];

  private readonly destroyRef = inject(DestroyRef);

  constructor(private readonly batchService: BatchService) {}

  ngOnInit(): void {
    this.batchService
      .list()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((batches) => {
        this.batches = batches;
      });
  }
}
