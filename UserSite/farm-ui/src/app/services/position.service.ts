import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AppConfig, AppConfiguration } from 'src/configuration';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class PositionService {
    TOKEN: any = 'a5cac09d-cea5-11ed-a3ed-eac62dba9bd9';
    API: any = 'https://online-gateway.ghn.vn/shiip/public-api/master-data/';

    constructor(
        @Inject(AppConfig) private readonly appConfig: AppConfiguration,
        private router: Router, private http: HttpClient,
    ) { }

    getListCity(): Observable<any> {
        return this.http.get<any>(this.API + 'province', {
            headers: new HttpHeaders().set('Token', `${this.TOKEN}`),
        }).pipe(map((z) => { return z; }));
    }

    getListDistrict(req: any): Observable<any> {
        return this.http.post<any>(this.API + 'district', req, {
            headers: new HttpHeaders().set('Token', `${this.TOKEN}`),
        }).pipe(map((z) => { return z; }));
    }

    getListWard(req: any): Observable<any> {
        return this.http.post<any>(this.API + 'ward', req, {
            headers: new HttpHeaders().set('Token', `${this.TOKEN}`),
        }).pipe(map((z) => { return z; }));
    }
}