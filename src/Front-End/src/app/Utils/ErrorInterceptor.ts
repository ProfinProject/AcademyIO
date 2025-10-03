import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { LocalStorageUtils } from './localstorage';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
   private localStorageUtil: LocalStorageUtils;

  constructor(
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.localStorageUtil = new LocalStorageUtils(this.platformId);
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(req).pipe(catchError(error => {
      if (error instanceof HttpErrorResponse) {

        if (error.status === 401) {
          this.localStorageUtil.cleanLocalDataUser();
          this.router.navigate(['/account/login'], { queryParams: { returnUrl: this.router.url } });
        }
        if (error.status === 403) {
          this.router.navigate(['/not-allowed']);
        }
      }

      return throwError(() => error);
    }));
  }
}
