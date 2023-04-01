import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerService } from './services/customer.services';
import { CustomerDetailComponent } from './component/customer-detail/customer-detail.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CustomersComponent } from './component/customers/customers.component';

@NgModule({
  declarations: [CustomersComponent, CustomerDetailComponent],
  imports: [CommonModule, FormsModule, HttpClientModule],
  providers: [CustomerService],
  exports: [CustomersComponent, CustomerDetailComponent],
})
export class CustomerModule {}
