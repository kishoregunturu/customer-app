import { Injectable } from '@angular/core';
import { Query, toBoolean } from '@datorama/akita';
import { SessionStore, SessionState } from './session.store';

@Injectable({
	providedIn: 'root'
})
export class SessionQuery extends Query<SessionState> {
	isLoggedIn$ = this.select((state) => toBoolean(state.token));
	userId$ = this.select((state) => state.userId);

	constructor(store: SessionStore) {
		super(store);
	}

	isLoggedIn() {
		return toBoolean(this.store.getValue().token);
	}

    userId() {
		return this.store.getValue().userId;
	}

    token() {
		return this.store.getValue().token;
	}
}
