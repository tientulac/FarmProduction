import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AppConfig, AppConfiguration } from 'src/configuration';
import { ReponseAPI } from '../entities/ResponseAPI';

@Injectable({
    providedIn: 'root'
})
export class BaseService<T> {

    TOKEN_LOCATION: any = 'a5cac09d-cea5-11ed-a3ed-eac62dba9bd9';
    API_LOCATION: any = 'https://online-gateway.ghn.vn/shiip/public-api/master-data/';

    constructor(
        @Inject(AppConfig) private readonly appConfig: AppConfiguration,
        private router: Router,
        private http: HttpClient,
    ) {
    }

    getByRequest(url: string, request: any | null): Observable<ReponseAPI<T[]>> {
        return this.http.post<ReponseAPI<T[]>>(this.appConfig.API + url, request).pipe(map((res) => { return res; }));
    }

    getAll(url: string): Observable<ReponseAPI<T[]>> {
        return this.http.get<ReponseAPI<T[]>>(this.appConfig.API + url).pipe(map((res) => { return res; }));
    }

    export(url: string): Observable<ReponseAPI<ArrayBuffer>> {
        return this.http.get<ReponseAPI<ArrayBuffer>>(this.appConfig.API + url + '/export').pipe(map((res) => { return res; }));
    }

    getById(url: string): Observable<ReponseAPI<T>> {
        return this.http.get<ReponseAPI<T>>(this.appConfig.API + url).pipe(map((res) => { return res; }));
    }

    save(url: string, entity: T): Observable<ReponseAPI<T>> {
        return this.http.post<ReponseAPI<T>>(this.appConfig.API + url, entity).pipe(map((res) => { return res; }));
    }

    delete(url: string): Observable<ReponseAPI<T>> {
        return this.http.delete<ReponseAPI<T>>(this.appConfig.API + url).pipe(map((res) => { return res; }));
    }

    search(url: string, request: any): Observable<ReponseAPI<T[]>> {
        return this.http.post<ReponseAPI<T[]>>(this.appConfig.API + url + '/search', request).pipe(map((res) => { return res; }));
    }

    getListCity(): Observable<any> {
        return this.http.get<any>(this.API_LOCATION + 'province', {
            headers: new HttpHeaders().set('Token', `${this.TOKEN_LOCATION}`),
        }).pipe(map((z) => { return z; }));
    }

    getListDistrict(req: any): Observable<any> {
        return this.http.post<any>(this.API_LOCATION + 'district', req, {
            headers: new HttpHeaders().set('Token', `${this.TOKEN_LOCATION}`),
        }).pipe(map((z) => { return z; }));
    }

    getListWard(req: any): Observable<any> {
        return this.http.post<any>(this.API_LOCATION + 'ward', req, {
            headers: new HttpHeaders().set('Token', `${this.TOKEN_LOCATION}`),
        }).pipe(map((z) => { return z; }));
    }

    getShipPayment(req: any): Observable<any> {
        return this.http.post<any>('https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee', req, {
            headers: new HttpHeaders().set('Token', `${this.TOKEN_LOCATION}`),
        }).pipe(map((z) => { return z; }));
    }
}

