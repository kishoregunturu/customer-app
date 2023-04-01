import { EntityStore, EntityState, PaginationResponse } from '@datorama/akita';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { CustomerStore } from '../store/customer.store';
import { Customer } from '../model/customer.model';
import { environment } from 'src/environments/environment.development';



@Injectable()
export class CustomerService {

  http: HttpClient;

  store: CustomerStore;

  constructor(http: HttpClient, store: CustomerStore) {
    this.http = http;
    this.store = store;
  }

  getAllCustomers(page:any): Observable<PaginationResponse<Customer>> {
    return this.http.post<PaginationResponse<Customer>>(`${environment.apiUrl}/customer/all`,page);
  }

  getCustomer(id:string): Observable<Customer> {
    return this.http.get<Customer>(`${environment.apiUrl}/customer/${id}`).pipe(
      tap(customer => {
        this.store.setActive(customer?.id);
      })
    );
  }

  createCustomer(customer: Customer): Observable<any> {
    return this.http.post<Customer>(`${environment.apiUrl}/customer`, customer).pipe(
      tap(value => {
        this.store.add([value]);
      })
    );
  }

  deleteCustomer(customerId: string): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/customer/` + customerId).pipe(
      tap(result => {
        this.store.remove(customerId);
      })
    );
  }

  updateCustomer(customerId: string, customer: Customer): Observable<any> {
    return this.http.put(`${environment.apiUrl}/customer`, customer).pipe(
      tap(result => {
        this.store.update(customerId, customer);
      })
    );
  }
}