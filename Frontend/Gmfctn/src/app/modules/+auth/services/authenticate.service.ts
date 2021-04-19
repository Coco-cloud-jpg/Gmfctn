import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { catchError, take, tap } from 'rxjs/operators';

import { Tokens } from '../../../shared/models/token';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService implements OnDestroy{
  tokens$: BehaviorSubject<Tokens> = new BehaviorSubject(null as unknown as Tokens);
  isErrorInAuthorization$: BehaviorSubject<boolean> = new BehaviorSubject(null as unknown as boolean);
  private apiUrl = 'https://localhost:44349/';

  constructor(private httpClient: HttpClient) { }

  ngOnDestroy(): void {
    this.tokens$.complete();
    this.isErrorInAuthorization$.complete();
  }

  authenticate(Login: string, Password: string): Observable<string[]> {
    return this.httpClient
      .post<string[]>(`${this.apiUrl}api/auth/login?Login=${Login}&Password=${Password}`, '')
      .pipe(tap(tokens => {

        const newTokens: Tokens = {token: tokens[0], aTTL: tokens[1], refreshToken: tokens[2]};

        localStorage.clear();
        localStorage.setItem('AToken', tokens[0]);
        localStorage.setItem('UTC_TTL', tokens[1]);
        localStorage.setItem('RToken', tokens[2]);

        this.tokens$.next(newTokens);
      }, catchError( (err: HttpErrorResponse) => of(err))), take(1));
  }
}
