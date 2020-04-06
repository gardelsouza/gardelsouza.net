import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators'

@Injectable()
export class ContactService {

    apiUrl: string = 'http://localhost:8080';

    constructor(private httpClient: HttpClient) { }

    public sendMail(text): Observable<any> {
        var API_URL = `${this.apiUrl}/sendMail`;

        return this.httpClient.post(API_URL, { "text": text }).pipe(
            map(res => { 
                return res 
            }), catchError(err => {
                return throwError('Erro ao enviar mensagem.');
            })
        );
    }
}