import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AppConfig, AppConfiguration } from 'src/configuration';
import { ReponseAPI } from '../entities/ResponseAPI';

@Injectable({
    providedIn: 'root',
})
export class UploadImageService {


    constructor(
        @Inject(AppConfig) private readonly appConfig: AppConfiguration,
        private router: Router,
        private http: HttpClient,
    ) {
    }

    setHeader() {
        return new HttpHeaders().set('Authorization', `Bearer ${''}`)
    }

    upload(formData: FormData): Observable<any> {
        return this.http.post<any>(this.appConfig.API + 'Upload/UploadImage', formData, {
            headers: this.setHeader(),
        }).pipe(map((res) => { return res; }));
    }

    getImgUpload(fileName: string): Observable<any> {
        return this.http.get<any>(this.appConfig.API + 'Upload/GetImage?imgName=' + fileName, {
            headers: this.setHeader(),
        }).pipe(map((res) => { return res; }));
    }
}

