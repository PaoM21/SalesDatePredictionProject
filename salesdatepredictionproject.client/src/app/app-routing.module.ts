import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersComponent } from './components/customers/customers.component';
import { OrdersComponent } from './components/orders/orders.component';
import { NewOrderComponent } from './components/new-order/new-order.component';

const routes: Routes = [
  { path: '', component: CustomersComponent },
  { path: 'orders/:id', component: OrdersComponent },
  { path: 'new-order/:id', component: NewOrderComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
