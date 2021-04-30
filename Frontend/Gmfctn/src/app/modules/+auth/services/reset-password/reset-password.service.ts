import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, take } from 'rxjs/operators';
import { apiUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService implements OnDestroy{

  error$: BehaviorSubject<HttpErrorResponse> = new BehaviorSubject(null as unknown as HttpErrorResponse);

  constructor(private httpClient: HttpClient) { }

  ngOnDestroy(): void {
    this.error$.complete();
  }

  sendRequest(email: string): Observable<string> {
    return this.httpClient
    .post<string>(`${apiUrl}api/auth/send-request?Email=${email}`, '')
    .pipe(catchError( err => {
      this.error$.next(err);
      return of('error');
    }));
  }

  resetPassword(body: object): Observable<string> {
    return this.httpClient
    .post<string>(`${apiUrl}api/auth/reset`, body)
    .pipe(catchError( err => {
      this.error$.next(err);
      return of('error');
    }));
  }
}
