import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { AuthenticationService } from '../services/auth.service';
import { AuthfakeauthenticationService } from '../services/authfake.service';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(
        private authenticationService: AuthenticationService,
        private authfackservice: AuthfakeauthenticationService,
        private router: Router
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (environment.defaultauth === 'firebase') {
            const currentUser = this.authenticationService.currentUser();            
            if (currentUser && currentUser.token) {
                request = request.clone({
                    setHeaders: {
                        Authorization: `Bearer ${currentUser.token}`
                    }
                });                
            }
        } else {
            // add authorization header with jwt token if available
            let currentUser = this.authfackservice.currentUserValue;

            if (currentUser && currentUser.jwtToken) {
                currentUser.token = currentUser.jwtToken.token;

                request = request.clone({
                    setHeaders: {
                        Authorization: `Bearer ${currentUser.token}`
                    }
                });
            }
        }

        return next.handle(request).pipe(tap(event => {
            let e : any = event;             

            if(e.status == 401){
                this.router.navigate(['/account/login'], { queryParams: { statusCode: 401 } });
            }

            if(e.status == 403){
                this.router.navigate(['/transaction']);
            }
        }, 
        (error: any) => {            
            if(error.status == 401){
                this.router.navigate(['/account/login'], { queryParams: { statusCode: 401 } });
            }

            if(error.status == 403){
                this.router.navigate(['/transaction']);
            }
        }));;
    }
}
