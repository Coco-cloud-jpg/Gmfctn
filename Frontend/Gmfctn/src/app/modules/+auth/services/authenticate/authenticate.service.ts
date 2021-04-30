import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { UrlTree } from '@angular/router';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { catchError, take, tap } from 'rxjs/operators';
import { apiUrl } from 'src/environments/environment';
import { Tokens } from '../../../../shared/models/token';


@Injectable({
  providedIn: 'root'
})
export class AuthenticateService implements OnDestroy{
  tokens$: BehaviorSubject<Tokens> = new BehaviorSubject(null as unknown as Tokens);
  isErrorInAuthorization$: BehaviorSubject<boolean> = new BehaviorSubject(null as unknown as boolean);

  constructor(private httpClient: HttpClient) { }

  ngOnDestroy(): void {
    this.tokens$.complete();
    this.isErrorInAuthorization$.complete();
  }

  authenticate(Login: string, Password: string): Observable<string[]> {
    const body = {Login: Login, Password: Password};

    return this.httpClient
      .post<string[]>(`${apiUrl}api/auth/login`, body)
      .pipe(tap(tokens => {

        const newTokens: Tokens = {token: tokens[0], aTTL: tokens[1], refreshToken: tokens[2]};

        localStorage.clear();
        localStorage.setItem('AToken', tokens[0]);
        localStorage.setItem('UTC_TTL', tokens[1]);
        localStorage.setItem('RToken', tokens[2]);

        this.tokens$.next(newTokens);
      }, catchError( (err: HttpErrorResponse) => of(err))), take(1));
  }

  isLoggedIn(): boolean {
    if (this.isTokenStillActive())
    {
      const tokens: Tokens = {
        token: localStorage.getItem('AToken') ?? '',
        aTTL: localStorage.getItem('UTC_TTL') ?? '',
        refreshToken: localStorage.getItem('RToken') ?? '',
      };

      if (tokens.token === '' || tokens.aTTL === '' || tokens.refreshToken === '') {
        return false;
      }
      else {
        this.tokens$.next(tokens);

        return true;
      }
    }

    return false;
  }

  isTokenStillActive(): boolean {
    const expiringDate = new Date(new Date(localStorage.getItem('UTC_TTL') + '').toUTCString());
    const now = new Date(new Date().toUTCString());

    return expiringDate > now;
  }
}
