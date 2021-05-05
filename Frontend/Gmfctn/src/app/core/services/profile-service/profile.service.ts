import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user';
import { apiUrl } from 'src/environments/environment';
import { FileService } from '../file-service/file.service';


@Injectable({
  providedIn: 'root'
})
export class ProfileService implements OnDestroy  {
  currentUser$: BehaviorSubject<User> = new BehaviorSubject(null as unknown as User);

  constructor(private httpClient: HttpClient, private fileService: FileService) {}

  getUserInfo(): Observable<User> {
    return this.httpClient
      .get<User>(`${apiUrl}api/profile/user_info`)
      .pipe(tap(user => {
        const userWithAvatar = this.loadUserExtraData(user);
        this.currentUser$.next(userWithAvatar);
      }));
  }

  loadUserExtraData(user: User): User {
    this.fileService.loadFile(user.avatarId)
      .subscribe(res => user.avatarId = !!res.url ? `${apiUrl}${res.url}` : '');
    user.badges = user.achievements?.length;

    user.achievements?.forEach(achievement => {
      this.fileService.loadFile(achievement.iconId)
        .subscribe(res => achievement.iconId = !!res.url ? `${apiUrl}${res.url}` : '');
    });

    for (let i = 0; i < (user.achievements?.length ?? 0); ++i) {
      (user.achievements ?? [])[i].time = new Date((user.datesCreation ?? [])[i]);
    }

    return user;
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
