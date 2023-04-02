import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, timer } from 'rxjs';
import { mapTo, tap } from 'rxjs/operators';
import { SessionState, SessionStore } from '../store/session.store';
import { environment } from 'src/environments/environment';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Injectable({
  providedIn: 'root',
})
export class SessionService {
  http: HttpClient;

  store: SessionStore;

  constructor(http: HttpClient, store: SessionStore) {
    this.http = http;
    this.store = store;
  }

  login(): Observable<any> {
    return this.http.post<SessionState>(`${environment.apiUrl}/login`, {}).pipe(
      untilDestroyed(this),
      tap((e) => {
        this.store.login(e);
      })
    );
  }

  logout() {
    this.store.logout();
  }
}
