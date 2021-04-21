import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, throwError } from 'rxjs';
import { catchError, skip, switchMap, take } from 'rxjs/operators';
import { AuthenticateService } from 'src/app/modules/+auth/services/authenticate.service';


@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor {
  constructor(private authenticateService: AuthenticateService, private router: Router) { }

  private handleAuthError(err: HttpErrorResponse): Observable<any> {
    if (err.status === 401 || err.status === 403) {
        this.authenticateService.isErrorInAuthorization$.next(true);
        this.router.navigateByUrl(`/auth/sign-in`);
        return of(err.message);
    }

    return throwError(err);
}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.authenticateService.isErrorInAuthorization$.next(false);
    return this.authenticateService.tokens$.pipe(take(1),switchMap((tokens) => {
      if (!tokens) {
        return next.handle(req).pipe(catchError(x => this.handleAuthError(x)));
      }

      const setHeaders = {
        authorization: 'Bearer ' + tokens.token
      };

      const finalReq = req.clone({ setHeaders });

      return next.handle(finalReq);
    }));
  }

}
