import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { Address } from '../../features/location/models/address';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AddressService } from '../../features/location/services/address.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-address-add',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './address-add.component.html',
  styleUrl: './address-add.component.scss',
})
export class AddAddressComponent implements OnInit {
  addressForm: FormGroup = null!;
  message: string | null = null;

  private readonly destroyRef = inject(DestroyRef);

  constructor(private readonly addressService: AddressService) {}

  ngOnInit(): void {
    this.addressForm = new FormGroup({
      addressText: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(500),
      ]),
      district: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100),
      ]),
      city: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100),
      ]),
    });
  }

  get addressText() {
    return this.addressForm.get('addressText');
  }

  get district() {
    return this.addressForm.get('district');
  }

  get city() {
    return this.addressForm.get('city');
  }

  onAdd() {
    this.addressForm.markAllAsTouched();

    if (this.addressForm.valid) {
      this.addressForm.markAsPending();

      this.addressService
        .add(this.addressForm.value)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe((address) => {
          this.addressForm.reset();
          this.message = `The address ${address.addressText}, ${address.district}, ${address.city} had been added`;

          console.log('The address had been added: %o', address);

          setTimeout(() => {
            this.message = null;
          }, 3000);
        });
    }
  }
}
