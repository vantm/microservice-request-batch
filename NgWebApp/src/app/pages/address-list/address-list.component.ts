import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { AddressService } from '../../features/location/services/address.service';
import { Address } from '../../features/location/models/address';
import { CommonModule } from '@angular/common';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-address-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './address-list.component.html',
  styleUrl: './address-list.component.scss',
})
export class AddressListComponent implements OnInit {
  addresses: Address[] = [];

  private readonly destroyRef = inject(DestroyRef);

  constructor(private readonly addressService: AddressService) {}

  ngOnInit(): void {
    this.addressService
      .list()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((addresses) => {
        this.addresses = addresses;
      });
  }
}
