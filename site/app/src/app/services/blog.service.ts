import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  apiUrl: string = 'https://dev.to/api';

  constructor(private httpClient: HttpClient) { }

  public get5Posts(): Observable<any> {
    var API_URL = `${this.apiUrl}/articles?username=gardelsouza&page=1&per_page=5`;

    return this.httpClient.get(API_URL).pipe(
        map(res => { 
            return res 
        }), catchError(err => {
            return throwError('Erro ao obter artigos.');
        })
    );

  }
}
