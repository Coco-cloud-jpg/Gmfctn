import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { apiUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private httpClient: HttpClient) { }

  loadFile(id: string): Observable<any> {
    return this.httpClient
      .post<any>(`${apiUrl}api/files/get-by-id?Id=${id}`, '')
      .pipe(catchError(() => of({url: ''}))).pipe();
  }
}
