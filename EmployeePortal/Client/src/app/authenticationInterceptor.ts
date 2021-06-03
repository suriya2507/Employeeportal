import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, of, throwError} from 'rxjs';
import {TOKEN_STORAGE_KEY} from './user.service';
import {catchError} from 'rxjs/operators';
import {Router} from '@angular/router';
import {Injectable} from '@angular/core';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
    constructor(private router: Router) {
    }


    handleUnauthorized(err: HttpErrorResponse): Observable<any> {
        if (err.status === 401) {
            localStorage.removeItem(TOKEN_STORAGE_KEY);
            this.router.navigateByUrl('/home');
            return of(err.message);
        }

        return throwError(err);
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const token = localStorage.getItem(TOKEN_STORAGE_KEY);
        if (token) {
            const headers = req.headers.set('Authorization', `bearer ${token}`);
            req = req.clone({
                headers: headers
            });
        }

        return next.handle(req).pipe(catchError(x => this.handleUnauthorized(x)));
    }
}
