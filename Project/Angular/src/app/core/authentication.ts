import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { mapToMapExpression } from '@angular/compiler/src/render3/util';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs/internal/observable/throwError';
import { CommonService } from './services/common.service';

// This will stop redirecting page without logged in 
@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private commonService: CommonService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUser = this.commonService.GetCurrentUser();
        if (currentUser) {
            // logged in so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/login']);
        return false;
    }
}



@Injectable()
export class BeforeApiCall implements HttpInterceptor {
    constructor(private commonService: CommonService, private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add auth header with jwt if user is logged in and request is to api url
        const currentUser = this.commonService.GetCurrentUser();
        const isLoggedIn = currentUser && currentUser.Token;
        const isApiUrl = request.url.startsWith(environment.apiUrl);
        if (isLoggedIn && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.Token}`
                }
            });
        }
        return next.handle(request).pipe(catchError((err: HttpErrorResponse) => {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                this.commonService.RemoveCurrentUser();
                this.router.navigate(['/login']);
                // location.reload(true);
            }
            if (err.status === 406) {
                this.commonService.ShowMessageByType("error", err.message)

            }
            if (err.status === 500 || err.status === 400) {
                this.commonService.ShowMessageByType("error", "Opps! Internal server error.")

            }

            const error = err.error.message || err.statusText;

            return throwError(error);
        }));
    }
}

@Injectable({ providedIn: 'root' })
export class AuthGuardLoggedIn implements CanActivate {
    constructor(
        private router: Router,
        private commonService: CommonService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUser = this.commonService.GetCurrentUser();
        if (currentUser) {
            this.router.navigate(['/dashboard']);
            return false;
        } else {
            return true;
        }
    }
}
