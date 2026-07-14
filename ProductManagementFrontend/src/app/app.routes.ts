import { Routes } from '@angular/router';

import { MainLayout } from './core/layout/main-layout/main-layout';
import { ProductList } from './features/products/pages/product-list/product-list';
import { ProductDetails } from './features/products/pages/product-details/product-details';
import { ProductCreate } from './features/products/pages/product-create/product-create';
import { ProductEdit } from './features/products/pages/product-edit/product-edit';
import { DashboardComponent } from './features/dashboard/pages/dashboard/dashboard';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    children: [
        { path: '', component: DashboardComponent },
        { path: 'products', component: ProductList },
        { path: 'products/new', component: ProductCreate },
        { path: 'products/:id/edit', component: ProductEdit },
        { path: 'products/:id', component: ProductDetails }, 
    ]
  },

  { path: '**', redirectTo: '' }
];