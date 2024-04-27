import { Component } from '@angular/core';
import { LocationModule } from './features/location/location.module';
import { ProductModule } from './features/product/product.module';
import { WarehouseModule } from './features/warehouse/warehouse.module';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    CoreModule,
    SharedModule,
    LocationModule,
    ProductModule,
    WarehouseModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'NgWebApp';
}
