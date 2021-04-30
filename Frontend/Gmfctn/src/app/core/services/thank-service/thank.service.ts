import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Thank } from 'src/app/shared/models/thank';
import { apiUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ThankService implements OnDestroy {
  thank$: BehaviorSubject<Thank> = new BehaviorSubject(null as unknown as Thank);

  constructor(private httpClient: HttpClient) { }

  ngOnDestroy(): void {
    this.thank$.complete();
  }

  sendThank(id: string, message: string): Observable<any> {
    return this.httpClient.post<any>(`${apiUrl}api/thank?Text=${message}&ToUserId=${id}`, '')
    .pipe();
  }

  getLastThank(): Observable<Thank> {
    return this.httpClient.get<Thank>(`${apiUrl}api/thank`)
    .pipe(map(thank => {
      const thankWithUserAvatar = thank;

      if ( thankWithUserAvatar ) {
      this.httpClient
      .post<any>(`${apiUrl}api/files/get-by-id?Id=${thankWithUserAvatar.fromUser.avatarId}`, '')
      .pipe(catchError(() => {
        thankWithUserAvatar.fromUser.avatarId = '';

        return EMPTY;
      }))
      .subscribe(res => thankWithUserAvatar.fromUser.avatarId = !! res ? `${apiUrl}${res.url}` : '');
      }

      return thankWithUserAvatar;
    }));
  }
}
