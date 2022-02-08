import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { NotificationService } from '../services/notification.service';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
    constructor(private notificationService: NotificationService, private router: Router) {}

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        return next.handle(request).pipe(
            catchError((err: HttpErrorResponse) => {
                if (err.status === 401) {
                    window.location.href = '/account/login';
                } else if (err.status === 403) {
                    this.router.navigate(['/forbidden']).catch((e) => console.error(e));
                }
                let errorMessage = '';
                if (err.error instanceof ErrorEvent) {
                    errorMessage = `An error occurred: ${err.error.message}`;
                } else {
                    errorMessage = `
                    Server returned code: ${err.status}
                    Error message:
                    ${err.message}
                    Error(s):
                    ${JSON.stringify(err.error)}`;
                }
                this.notificationService.warn(errorMessage);

                return throwError(errorMessage);
            })
        );
    }
}
