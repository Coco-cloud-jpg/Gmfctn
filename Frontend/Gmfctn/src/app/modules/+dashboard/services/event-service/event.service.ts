import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FileService } from 'src/app/core/services/file-service/file.service';
import { EventIS } from 'src/app/shared/models/event';
import { apiUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private httpClient: HttpClient, private fileService: FileService) { }

  getAllEvents(): Observable<EventIS[]> {
    return this.httpClient.get<EventIS[]>(`${apiUrl}api/events`)
    .pipe(map(events => {
      events = this.loadEventsExtraData(events);

      return events;
    }));
  }

  loadEventsExtraData(events: EventIS[]): EventIS[] {
    events.forEach(event => {
      event.date = new Date(event.createdTime);
      this.fileService.loadFile(event.user.avatarId)
      .subscribe(res => event.user.avatarId = !!res.url ? `${apiUrl}${res.url}` : '');
    });

    return events;
  }
}
