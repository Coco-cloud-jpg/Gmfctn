import { HttpClient } from '@angular/common/http';
import { inject, Injectable, InjectFlags, OnDestroy, Type } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user';
import { apiUrl } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ProfileService implements OnDestroy  {
  currentUser$: BehaviorSubject<User> = new BehaviorSubject(null as unknown as User);

  constructor(private httpClient: HttpClient) {}

  getUserInfo(): Observable<User> {
    return this.httpClient
      .get<User>(`${apiUrl}api/profile/user_info`)
      .pipe(tap(user => {
        const userWithAvatar = this.loadUserExtraData(user);
        this.currentUser$.next(userWithAvatar);
      }));
  }

  loadUserExtraData(user: User): User {
    const userWithAvatar = user;
    this.httpClient
      .post<any>(`${apiUrl}api/files/get-by-id?Id=${user.avatarId}`, '')
      .pipe(catchError(() => {
        userWithAvatar.avatarId = '';

        return EMPTY;
      }))
      .subscribe(res => userWithAvatar.avatarId = `${apiUrl}${res.url}`);
    userWithAvatar.badges = userWithAvatar.achievements?.length;

    userWithAvatar.achievements?.forEach(achievement => {
      this.httpClient
      .post<any>(`${apiUrl}api/files/get-by-id?Id=${achievement.iconId}`, '')
      .pipe(catchError(() => {
        achievement.iconId = '';

        return EMPTY;
      }))
      .subscribe(res => achievement.iconId = !!res ? `${apiUrl}${res.url}` : '');
    });

    for (let i = 0; i < (userWithAvatar.achievements?.length ?? 0); ++i) {
      (userWithAvatar.achievements ?? [])[i].time = new Date((user.datesCreation ?? [])[i]);
    }

    return userWithAvatar;
  }

  updateUser(data: object): Observable<any> {
    return this.httpClient.put<any>(`${apiUrl}api/profile/update`, data)
    .pipe(tap(() => {
      this.currentUser$.next({...this.currentUser$.value, ...data});
    }));
  }

  ngOnDestroy(): void {
    this.currentUser$.complete();
  }
}
