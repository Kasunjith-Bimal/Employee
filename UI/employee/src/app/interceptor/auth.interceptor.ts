import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { AuthorizeService } from '../services/authorize.service';
import { Router } from '@angular/router';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authorizeService: AuthorizeService,private router : Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.authorizeService.getAccessToken();
    if (token) {
     var tokenDetail = JSON.parse(token);
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${tokenDetail.accessToken}`,
        },
      });
    }

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        // Handle specific status codes or errors
        if (error.status === 401) {
          this.router.navigate(['authorize/login'])
         // console.log(error);
          // Token might be expired or invalid
          // Refresh the token or navigate to login
        }
        // For password change, you can add logic here or in a specific service method
        return throwError(error);
      })
    );
  }
}