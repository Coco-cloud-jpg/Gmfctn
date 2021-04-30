import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Achievement } from 'src/app/shared/models/achievement';
import { UserSI } from 'src/app/shared/models/user-short-info';
import { apiUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService implements OnDestroy{
  usersList$: BehaviorSubject<UserSI[]> = new BehaviorSubject(null as unknown as UserSI[]);

  constructor(private httpClient: HttpClient) { }

  ngOnDestroy(): void {
    this.usersList$.complete();
  }

  getTopFiveUsers(): Observable<UserSI[]> {
    return this.httpClient
    .get<UserSI[]>(`${apiUrl}api/user/get_all_users_info`)
    .pipe(map(users => {
      users.sort((a, b) => b.xp - a.xp);
      users = users.slice(0, 5);
      const usersWithAvatar = this.loadUserExtraData(users);
      this.usersList$.next(usersWithAvatar);

      return usersWithAvatar;
    }));
  }

  loadUserExtraData(users: UserSI[]): UserSI[] {
    const usersWithAvatar = users;

    usersWithAvatar.forEach(user => {
      this.httpClient
      .post<any>(`${apiUrl}api/files/get-by-id?Id=${user.avatarId}`, '')
      .pipe(catchError(() => {
        user.avatarId = '';

        return EMPTY;
      }))
      .subscribe(res => user.avatarId = !!res ? `${apiUrl}${res.url}` : '');
    });

    return usersWithAvatar;
  }

  getUserById(id: string): Observable<UserSI> {
    return this.httpClient.get<UserSI>(`${apiUrl}api/user/get_user?Id=${id}`)
    .pipe(
      map(user => {
        const userWithAvatar = user;
        this.httpClient
        .post<any>(`${apiUrl}api/files/get-by-id?Id=${userWithAvatar.avatarId}`, '')
        .pipe(catchError(() => {
          userWithAvatar.avatarId = '';

          return EMPTY;
        }))
        .subscribe(res => userWithAvatar.avatarId = !! res ? `${apiUrl}${res.url}` : '');

        return userWithAvatar;
      })
    );
  }

  passwordChange(passwords: object): Observable<any> {
    return this.httpClient.put<object>(`${apiUrl}api/profile/change-password`, passwords)
    .pipe(tap(() => {
      localStorage.clear();
      window.location.reload();
    }));
  }
}
