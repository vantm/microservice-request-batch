import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LocationHeaderComponent } from '../../core/components/location-header/location-header.component';

@Component({
  selector: 'app-address-home',
  standalone: true,
  imports: [RouterOutlet, LocationHeaderComponent],
  templateUrl: './address-home.component.html',
  styleUrl: './address-home.component.scss',
})
export class HomeComponent {}
