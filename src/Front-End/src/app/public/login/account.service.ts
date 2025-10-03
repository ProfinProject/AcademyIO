import { Inject, PLATFORM_ID, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './models/user';

import { Observable } from 'rxjs';
import { catchError, map } from "rxjs/operators";
import { BaseService } from '../../services/base.service';
import { registerUser } from './models/registerUser';

@Injectable()
export class AccountService extends BaseService {
      constructor(
            private http: HttpClient,
            @Inject(PLATFORM_ID) platformId: Object
        ) {
            super(platformId); // ✅ Passando corretamente
        }


    register(user: registerUser): Observable<User> {
        let response = this.http
            .post(this.UrlServiceV1 + 'auth/register', user, this.getHeaderJson())
            .pipe(
                map(this.extractData),
                catchError(this.serviceError));

        return response;
    }

    login(user: User): Observable<User> {
        let response = this.http
            .post(this.UrlServiceV1 + 'auth/login', user, this.getHeaderJson())
            .pipe(
                map(this.extractData),
                catchError(this.serviceError));

        return response;
    }
}