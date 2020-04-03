import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ContactService {

    apiUrl: string = 'http://localhost:8080';

    constructor(private httpClient: HttpClient) { }

    public sendMail(text): any {
        var API_URL = `${this.apiUrl}/sendMail`;

        this.httpClient.post(API_URL, { "text": text }).subscribe(
            res => {
                return res;
            }, err => {
                return err;
            }
        );
    }
}