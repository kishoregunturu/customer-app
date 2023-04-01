import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerDetailComponent } from './customer/component/customer-detail/customer-detail.component';
import { CustomersComponent } from './customer/component/customers/customers.component';
import { CustomerModule } from './customer/customer.module';

const routes = [
  {
    path: 'customers',
    component: CustomersComponent,
  },
  { path: 'create', component: CustomerDetailComponent },
  {
    path: 'customer/:id',
    component: CustomerDetailComponent,
  },
  { path: '**', redirectTo: 'customers' },
];

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, CustomerModule, RouterModule.forRoot(routes)],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
