import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable()
export class SetHeaderInterceptor implements HttpInterceptor {

  TOKEN: any = '';

  constructor(
    public toastr: ToastrService,
    public router: Router
  ) {
    this.TOKEN = localStorage.getItem('TOKEN');
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const modifiedRequest = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.TOKEN}`
      }
    });

    return next.handle(modifiedRequest).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          
        }
      }),
      catchError((error: any) => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          // this.router.navigateByUrl('/login');
          this.toastr.warning('Bạn không có quyền thực hiện chức năng này. Vui lòng liên hệ quản trị viên !');
        }
        return throwError(error);
      })
    );
  }
}
