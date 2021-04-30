import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Thank } from 'src/app/shared/models/thank';
import { apiUrl } from 'src/environments/environment';
import { FileService } from '../file-service/file.service';

@Injectable({
  providedIn: 'root'
})
export class ThankService implements OnDestroy {
  thank$: BehaviorSubject<Thank> = new BehaviorSubject(null as unknown as Thank);

  constructor(private httpClient: HttpClient, private fileService: FileService) { }

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
      this.fileService.loadFile(thankWithUserAvatar.fromUser.avatarId)
      .subscribe(res => thankWithUserAvatar.fromUser.avatarId = !! res.url ? `${apiUrl}${res.url}` : '');
      }

      return thankWithUserAvatar;
    }));
  }
}
