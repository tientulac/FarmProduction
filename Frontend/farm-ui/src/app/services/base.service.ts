import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AppConfig, AppConfiguration } from 'src/configuration';
import { ReponseAPI } from '../entities/ResponseAPI';

@Injectable({
    providedIn: 'root',
})
export class BaseService<T> {

    TOKEN: any = 'a5cac09d-cea5-11ed-a3ed-eac62dba9bd9';
    API: any = 'https://online-gateway.ghn.vn/shiip/public-api/master-data/';

    constructor(
        @Inject(AppConfig) private readonly appConfig: AppConfiguration,
        private router: Router,
        private http: HttpClient,
    ) {
    }

    setHeader() {
        return new HttpHeaders().set('Authorization', `Bearer ${''}`)
    }

    getByRequest(url: string, request: any | null): Observable<ReponseAPI<T[]>> {
        return this.http.post<ReponseAPI<T[]>>(this.appConfig.API + url + '/get-by-request', request, {
            headers: this.setHeader(),
        }).pipe(map((res) => { return res; }));
    }

    getAll(url: string): Observable<ReponseAPI<T[]>> {
        return this.http.get<ReponseAPI<T[]>>(this.appConfig.API + url, {
            headers: this.setHeader(),
        }).pipe(map((res) => { return res; }));
    }

    getById(url: string): Observable<ReponseAPI<T>> {
        return this.http.get<ReponseAPI<T>>(this.appConfig.API + url, {
            headers: this.setHeader(),
        }).pipe(map((res) => { return res; }));
    }

    save(url: string, entity: T): Observable<ReponseAPI<T>> {
        return this.http.post<ReponseAPI<T>>(this.appConfig.API + url, entity, {
            headers: this.setHeader(),
        }).pipe(map((res) => { return res; }));
    }

    delete(url: string): Observable<ReponseAPI<T>> {
        return this.http.delete<ReponseAPI<T>>(this.appConfig.API + url, {
            headers: this.setHeader(),
        }).pipe(map((res) => { return res; }));
    }

    search(url: string, request: any): Observable<ReponseAPI<T[]>> {
        return this.http.post<ReponseAPI<T[]>>(this.appConfig.API + url + '/search', request, {
            headers: this.setHeader(),
        }).pipe(map((res) => { return res; }));
    }

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

    getShipPayment(req: any): Observable<any> {
        return this.http.post<any>('https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee', req, {
            headers: new HttpHeaders().set('Token', `${this.TOKEN}`),
        }).pipe(map((z) => { return z; }));
    }
}

