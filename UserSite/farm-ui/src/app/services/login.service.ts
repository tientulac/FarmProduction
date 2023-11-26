import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AppConfig, AppConfiguration } from 'src/configuration';
import { ReponseAPI } from '../entities/ResponseAPI';
import { UserAccountLogin } from '../entities/UserAccount.Entity';

@Injectable({
    providedIn: 'root',
})
export class LoginService {
    constructor(
        @Inject(AppConfig) private readonly appConfig: AppConfiguration,
        private router: Router,
        private http: HttpClient,
    ) {
    }

    login(request: any): Observable<ReponseAPI<UserAccountLogin>> {
        return this.http.post<ReponseAPI<UserAccountLogin>>(this.appConfig.API + 'UserAccount/Login', request).pipe(map((res) => { return res; }));
    }
}

