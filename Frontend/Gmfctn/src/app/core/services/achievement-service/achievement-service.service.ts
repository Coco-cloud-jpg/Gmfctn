import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable, of } from 'rxjs';
import { catchError, count, map, tap } from 'rxjs/operators';
import { Achievement } from 'src/app/shared/models/achievement';
import { apiUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AchievementServiceService implements OnDestroy {

  achievements$: BehaviorSubject<Achievement[]> = new BehaviorSubject(null as unknown as Achievement[]);

  constructor(private httpClient: HttpClient) { }

  ngOnDestroy(): void {
    this.achievements$.complete();
  }

  getAllAchievements(): Observable<Achievement[]> {
    return this.httpClient.get<Achievement[]>(`${apiUrl}api/achievement`)
    .pipe(tap(achievements => {
      this.achievements$.next(achievements);
    }));
  }

  sendRequest(body: object): Observable<object> {
    return this.httpClient.post(`${apiUrl}api/request/request`, body)
    .pipe();
  }

  getUserAchievementsById(id: string): Observable<Achievement[]> {
    return this.httpClient.get<Achievement[]>(`${apiUrl}api/achievement/get-user-achievements?UserId=${id}`)
    .pipe(map(achievements => this.loadAchievementsIcons(achievements)));
  }

  loadAchievementsIcons(achievements: Achievement[]): Achievement[] {
    const achievementsWithIcon = achievements;

    achievementsWithIcon.forEach(achievement => {
      this.httpClient
      .post<any>(`${apiUrl}api/files/get-by-id?Id=${achievement.iconId}`, '')
      .pipe(catchError(() => {
        achievement.iconId = '';

        return EMPTY;
      }))
      .subscribe(res => achievement.iconId = !!res ? `${apiUrl}${res.url}` : '');
    });

    return achievementsWithIcon;
  }
}
