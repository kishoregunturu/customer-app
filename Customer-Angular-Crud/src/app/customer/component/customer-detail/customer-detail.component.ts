import { Subscription } from 'rxjs';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as uuid from 'uuid';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { CustomerStore } from '../../store/customer.store';
import { CustomerService } from '../../services/customer.services';
import { Customer } from '../../model/customer.model';
import { CustomerQuery } from '../../store/customer.query';

@UntilDestroy()
@Component({
  selector: 'customer-detail',
  templateUrl: './customer-detail.component.html',
})
export class CustomerDetailComponent implements OnInit, OnDestroy {
  id?: string | null;
  customer?: any;
  errors?: any;

  constructor(
    private customerService: CustomerService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.customerService
        .getCustomer(this.id)
        .pipe(untilDestroyed(this))
        .subscribe((c) => {
          this.customer = c;
        });
    }
  }

  ngOnInit() {}

  onSubmit(submittedForm: any) {
    console.log(submittedForm.value);

    if (submittedForm.invalid) {
      return;
    }

    const customer: Customer = {
      id: this.id ? this.id : uuid.v4(),
      firstName: submittedForm?.value?.firstName,
      lastName: submittedForm?.value?.lastName,
      email: submittedForm?.value?.email,
    };

    this.id
      ? this.customerService
          .updateCustomer(this.id, customer)
          .pipe(untilDestroyed(this))
          .subscribe((result) => {
            if (result && result.errors?.length > 0) {
              this.errors = result.errors;
            } else this.router.navigateByUrl('/customers');
          })
      : this.customerService
          .createCustomer(customer)
          .pipe(untilDestroyed(this))
          .subscribe((result) => {
            if (result && result.errors?.length > 0) {
              this.errors = result.errors;
            } else this.router.navigateByUrl('/customers');
          });
  }

  ngOnDestroy() {}
}
