import { Injectable } from '@angular/core';
import { Store, StoreConfig } from '@datorama/akita';
import * as storage from '../storage';

export type SessionState = {
  token: string;
  userId: string;
};

export function createInitialSessionState(): SessionState {
  return {
    token: null,
    userId: null,
    ...storage.getSession(),
  };
}

@Injectable({ providedIn: 'root' })
@StoreConfig({ name: 'session' })
export class SessionStore extends Store<SessionState> {
  constructor() {
    super(createInitialSessionState());
  }

  login(session: SessionState) {
    this.update(session);
    storage.saveSession(session);
  }

  logout() {
    storage.clearSession();
    this.update(createInitialSessionState());
  }
}
