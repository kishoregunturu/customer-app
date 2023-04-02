import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerDetailComponent } from './customer/component/customer-detail/customer-detail.component';
import { CustomersComponent } from './customer/component/customers/customers.component';
import { CustomerModule } from './customer/customer.module';
import { LoginComponent } from './session/login/login.component';
import { AuthGuard } from './session/session.auth';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

const routes = [
  {
    path: 'customers',
    canActivate: [AuthGuard],
    component: CustomersComponent,
  },
  {
    path: 'create',
    canActivate: [AuthGuard],
    component: CustomerDetailComponent,
  },
  {
    path: 'customer/:id',
    canActivate: [AuthGuard],
    component: CustomerDetailComponent,
  },
  { path: '**', redirectTo: 'login' },

  {
    path: 'login',
    component: LoginComponent,
  },
];

@NgModule({
  declarations: [AppComponent, LoginComponent],
  imports: [
    BrowserModule,
    CustomerModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
  ],
  providers: [],

  bootstrap: [AppComponent],
})
export class AppModule {}
