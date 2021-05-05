import { Injectable } from '@angular/core';
import { CanActivate, UrlTree, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { AuthenticateService } from 'src/app/modules/+auth/services/authenticate/authenticate.service';

@Injectable({
  providedIn: 'root'
})
export class LoggedInGuard implements CanActivate {

  constructor(private authenticateService: AuthenticateService, private router: Router) { }

  canActivate(): Observable<boolean | UrlTree> {
    return this.authenticateService.isLoggedIn() ? of(this.router.createUrlTree(['/auth/sign-in'])) : of(true);
  }

}
