import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationHeaderComponent } from './location-header.component';

describe('HeaderComponent', () => {
  let component: LocationHeaderComponent;
  let fixture: ComponentFixture<LocationHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LocationHeaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LocationHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
