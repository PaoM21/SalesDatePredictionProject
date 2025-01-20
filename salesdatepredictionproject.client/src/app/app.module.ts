import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OrdersComponent } from './components/orders/orders.component';
import { NewOrderComponent } from './components/new-order/new-order.component';
import { CustomersComponent } from './components/customers/customers.component';

@NgModule({
  declarations: [
    AppComponent,
    CustomersComponent,
    OrdersComponent,
    NewOrderComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
