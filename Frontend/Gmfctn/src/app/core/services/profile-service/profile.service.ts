import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class ProfileService implements OnDestroy {
  currentUser$: BehaviorSubject<User> = new BehaviorSubject(null as unknown as User);

  private apiUrl = 'https://localhost:44349/';

  constructor(private httpClient: HttpClient) { }

  getUserInfo(): Observable<User> {
    return this.httpClient
      .get<User>(`${this.apiUrl}api/profile/user_info`)
      .pipe(tap(user => {
        this.currentUser$.next(user);
      }));
  }

  ngOnDestroy(): void {
    this.currentUser$.complete();
  }
}
