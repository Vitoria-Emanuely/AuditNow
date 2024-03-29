import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Login } from 'src/app/core/models/login.models';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
    })
};

@Injectable({ providedIn: 'root' })

export class LoginService {
    constructor(
        private http: HttpClient
    ) { }

    login(login: Login): Observable<any> {
        return this.http.post(
            `${environment.apiBaseAddress}Login`,
            login,
            httpOptions
        );
    }
}
