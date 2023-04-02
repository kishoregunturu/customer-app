import { tap, switchMap, filter } from 'rxjs/operators';
import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { Observable, Subscription } from 'rxjs';

import { Customer } from '../../model/customer.model';
import { CustomerState } from '../../store/customer.store';
import { CustomerService } from '../../services/customer.services';
import { CustomerQuery } from '../../store/customer.query';
import { Router } from '@angular/router';
import { PaginationResponse, PaginatorPlugin } from '@datorama/akita';
import { CUSTOMER_PAGINATOR } from '../../store/customer.paginator';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'app-Customers-list',
  templateUrl: './Customers.component.html',
})
export class CustomersComponent implements OnInit, OnDestroy {
  customerState?: CustomerState;
  perPage = 10;
  customers$?: Observable<PaginationResponse<any>>;
  pages = [10, 25, 50];
  activeId?: string;

  constructor(
    @Inject(CUSTOMER_PAGINATOR) public paginatorRef: PaginatorPlugin<Customer>,
    private customerService: CustomerService,
    private customerQuery: CustomerQuery,
    private router: Router
  ) {}

  ngOnInit() {
    this.selectCustomer();
    this.activeId = this.customerQuery.getActiveId();
  }

  selectCustomer() {
    //clear cache
    this.paginatorRef.clearCache();
    this.customers$ = this.paginatorRef.pageChanges.pipe(
      untilDestroyed(this),
      switchMap((page) => {
        const requestFn = () =>
          this.customerService.getAllCustomers({
            page,
            perPage: this.perPage,
          });

        return this.paginatorRef.getPage(requestFn);
      })
    );
  }

  deleteCustomer(CustomerId: string) {
    this.customerService
      .deleteCustomer(CustomerId)
      .pipe(untilDestroyed(this))
      .subscribe((result) => {
        console.log(result);
      });
  }

  showUpdateForm(customer: Customer) {
    this.customerQuery.setActive(customer);
    this.router.navigateByUrl(`/customer/${customer?.id}`);
  }

  ngOnDestroy() {
    this.paginatorRef.clearCache();
    this.paginatorRef.destroy();
  }
}
