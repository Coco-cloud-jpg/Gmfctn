import { Injectable } from '@angular/core';
import { CanActivate, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { AuthenticateService } from 'src/app/modules/+auth/services/authenticate.service';

@Injectable({
  providedIn: 'root'
})
export class SignInGuard implements CanActivate {

  constructor(private authenticateService: AuthenticateService, private router: Router) { }

  canActivate(): Observable<boolean | UrlTree> {
    return this.authenticateService.tokens$.pipe(take(1), map(tokens => {

      return !!tokens ? true : this.router.createUrlTree(['/auth/sign-in']);
    }));
  }
}
