import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/home/home.component').then((m) => m.HomeComponent),
  },
  {
    path: 'location',
    loadComponent: () =>
      import('./pages/address-home/address-home.component').then(
        (m) => m.HomeComponent
      ),
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./pages/address-list/address-list.component').then(
            (m) => m.AddressListComponent
          ),
      },
      {
        path: 'add',
        loadComponent: () =>
          import('./pages/address-add/address-add.component').then(
            (m) => m.AddAddressComponent
          ),
      },
    ],
  },
  {
    path: 'product',
    loadComponent: () =>
      import('./pages/product-home/product-home.component').then(
        (m) => m.ProductHomeComponent
      ),
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./pages/product-list/product-list.component').then(
            (m) => m.ProductListComponent
          ),
      },
      {
        path: 'add',
        loadComponent: () =>
          import('./pages/product-add/product-add.component').then(
            (m) => m.ProductAddComponent
          ),
      },
    ],
  },
  {
    path: 'warehouse',
    loadComponent: () =>
      import('./pages/warehouse-home/warehouse-home.component').then(
        (m) => m.WarehouseHomeComponent
      ),
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./pages/warehouse-list/warehouse-list.component').then(
            (p) => p.WarehouseListComponent
          ),
      },
      {
        path: 'add',
        loadComponent: () =>
          import('./pages/warehouse-add/warehouse-add.component').then(
            (p) => p.WarehouseAddComponent
          ),
      },
      {
        path: 'batch',
        loadComponent: () =>
          import('./pages/batch-list/batch-list.component').then(
            (p) => p.BatchListComponent
          ),
      },
      {
        path: 'batch-add',
        loadComponent: () =>
          import('./pages/batch-add/batch-add.component').then(
            (p) => p.BatchAddComponent
          ),
      },
    ],
  },
];
