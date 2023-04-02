import { Injectable } from '@angular/core';
import { ID, EntityStore, StoreConfig, EntityState } from '@datorama/akita';
import { Customer } from '../model/customer.model';

export interface CustomerState extends EntityState<Customer, string> {}

@Injectable({
  providedIn: 'root',
})
@StoreConfig({ name: 'Customers' })
export class CustomerStore extends EntityStore<CustomerState> {
  constructor() {
    super();
  }

  loadCustomers(customers: Customer[]) {
    this.set(customers);
  }
}
