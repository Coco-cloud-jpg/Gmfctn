import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { EventIS } from 'src/app/shared/models/event';
import { apiUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventServiceService {

  constructor(private httpClient: HttpClient) { }

  getAllEvents(): Observable<EventIS[]> {
    return this.httpClient.get<EventIS[]>(`${apiUrl}api/events`)
    .pipe(map(events => {
      events = this.loadEventsExtraData(events);
      return events;
    }));
  }

  loadEventsExtraData(events: EventIS[]): EventIS[] {
    const eventsWithUsersAvatar = events;

    eventsWithUsersAvatar.forEach(event => {
      event.date = new Date(event.createdTime);
      this.httpClient
      .post<any>(`${apiUrl}api/files/get-by-id?Id=${event.user.avatarId}`, '')
      .pipe(catchError(() => {
        event.user.avatarId = '';

        return EMPTY;
      }))
      .subscribe(res => event.user.avatarId = !!res ? `${apiUrl}${res.url}` : '');
    });

    return eventsWithUsersAvatar;
  }
}
