import { HttpInterceptorFn } from '@angular/common/http';
import { HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';

export const authInterceptor: HttpInterceptorFn = (
  req: HttpRequest<any>,
  next: HttpHandlerFn
) => {
  const authService = inject(AuthService);
  const authToken = authService.getAuthToken();
  if (authToken) {
    const authReq = req.clone({
      headers: req.headers.set('Authorization', 'Basic ' + authToken),
    });
    return next(authReq);
  }
  return next(req);
};
