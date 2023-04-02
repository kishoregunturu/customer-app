import { inject, InjectionToken } from '@angular/core';
import { PaginatorPlugin } from '@datorama/akita';

import { CustomerQuery } from './customer.query';

export const CUSTOMER_PAGINATOR = new InjectionToken('CUSTOMER_PAGINATOR', {
  providedIn: 'root',
  factory: () => {
    const customerQuery = inject(CustomerQuery);
    return new PaginatorPlugin(customerQuery).withControls().withRange();
  },
});
