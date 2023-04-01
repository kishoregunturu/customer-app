import { Injectable } from '@angular/core';
import { QueryEntity } from '@datorama/akita';

import { CustomerState, CustomerStore } from './customer.store';
import { Customer } from '../model/customer.model';


@Injectable({
  providedIn: 'root'
})
export class CustomerQuery extends QueryEntity<CustomerState> {

  constructor(store: CustomerStore) {
    super(store);
  }

  setActive(customer:Customer){
    this.store.updateActive(customer);
  }

}