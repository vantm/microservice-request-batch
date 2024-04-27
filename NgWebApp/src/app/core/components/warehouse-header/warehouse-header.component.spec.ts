import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseHeaderComponent } from './warehouse-header.component';

describe('HeaderComponent', () => {
  let component: WarehouseHeaderComponent;
  let fixture: ComponentFixture<WarehouseHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WarehouseHeaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WarehouseHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
