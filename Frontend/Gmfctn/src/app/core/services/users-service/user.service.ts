import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { UserSI } from 'src/app/shared/models/user-short-info';
import { apiUrl } from 'src/environments/environment';
import { FileService } from '../file-service/file.service';

@Injectable({
  providedIn: 'root'
})
export class UserService implements OnDestroy{
  usersList$: BehaviorSubject<UserSI[]> = new BehaviorSubject(null as unknown as UserSI[]);

  constructor(private httpClient: HttpClient, private fileService: FileService) { }

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
    users.forEach(user => {
      this.fileService.loadFile(user.avatarId)
      .subscribe(res => user.avatarId = !!res.url ? `${apiUrl}${res.url}` : '');
    });

    return users;
  }

  getUserById(id: string): Observable<UserSI> {
    return this.httpClient.get<UserSI>(`${apiUrl}api/user/get_user?Id=${id}`)
    .pipe(
      map(user => {
        this.fileService.loadFile(user.avatarId)
        .subscribe(res => user.avatarId = !! res.url ? `${apiUrl}${res.url}` : '');

        return user;
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
