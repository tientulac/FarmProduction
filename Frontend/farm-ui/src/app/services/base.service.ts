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
}

