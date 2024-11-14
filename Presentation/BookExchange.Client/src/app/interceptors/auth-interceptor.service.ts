import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Skip adding credentials for login request
    if (req.url.includes('/session/login')) {
      return next.handle(req);
    }

    // Clone the request to add the credentials
    const clonedRequest = req.clone({
      withCredentials: true
    });

    return next.handle(clonedRequest);
  }
}