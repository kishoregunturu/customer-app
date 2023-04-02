import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { SessionService } from './session/services/session.services';
import { SessionQuery } from './session/store/session.query';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'customer crud';
  loggedIn$: Observable<boolean>;

  constructor(
    private authService: SessionService,
    private authQuery: SessionQuery
  ) {
    this.loggedIn$ = this.authQuery.isLoggedIn$;
  }

  ngOnInit() {}

  logout() {
    this.authService.logout();
  }
}
