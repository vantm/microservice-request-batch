import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseHomeComponent } from './warehouse-home.component';

describe('HomeComponent', () => {
  let component: WarehouseHomeComponent;
  let fixture: ComponentFixture<WarehouseHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WarehouseHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WarehouseHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
