import { EntityStore, EntityState, PaginationResponse } from '@datorama/akita';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { CustomerStore } from '../store/customer.store';
import { Customer } from '../model/customer.model';
import { environment } from 'src/environments/environment.development';
import { SessionQuery } from 'src/app/session/store/session.query';

@Injectable()
export class CustomerService {
  http: HttpClient;
  headers: HttpHeaders;
  store: CustomerStore;

  constructor(
    http: HttpClient,
    store: CustomerStore,
    sessionQuery: SessionQuery
  ) {
    this.http = http;
    this.store = store;

    //headers
    this.headers = new HttpHeaders()
      .set('content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*')
      .set('Authorization',`Bearer ${sessionQuery.token()}`);
  }

  getAllCustomers(page: any): Observable<PaginationResponse<Customer>> {
    return this.http.post<PaginationResponse<Customer>>(
      `${environment.apiUrl}/customer/all`,
      page,{headers:this.headers}
    );
  }

  getCustomer(id: string): Observable<Customer> {
    return this.http.get<Customer>(`${environment.apiUrl}/customer/${id}`,{headers:this.headers}).pipe(
      tap((customer) => {
        this.store.setActive(customer?.id);
      })
    );
  }

  createCustomer(customer: Customer): Observable<any> {
    return this.http
      .post<Customer>(`${environment.apiUrl}/customer`, customer,{headers:this.headers})
      .pipe(
        tap((value) => {
          this.store.add([value]);
        })
      );
  }

  deleteCustomer(customerId: string): Observable<any> {
    return this.http
      .delete(`${environment.apiUrl}/customer/` + customerId,{headers:this.headers})
      .pipe(
        tap((result) => {
          this.store.remove(customerId);
        })
      );
  }

  updateCustomer(customerId: string, customer: Customer): Observable<any> {
    return this.http.put(`${environment.apiUrl}/customer`, customer,{headers:this.headers}).pipe(
      tap((result) => {
        this.store.update(customerId, customer);
      })
    );
  }
}
