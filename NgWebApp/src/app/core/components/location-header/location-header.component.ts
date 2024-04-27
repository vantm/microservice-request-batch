import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-location-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './location-header.component.html',
  styleUrl: './location-header.component.scss',
})
export class LocationHeaderComponent {}
