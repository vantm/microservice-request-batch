import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchAddComponent } from './batch-add.component';

describe('AddBatchComponent', () => {
  let component: BatchAddComponent;
  let fixture: ComponentFixture<BatchAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BatchAddComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BatchAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
