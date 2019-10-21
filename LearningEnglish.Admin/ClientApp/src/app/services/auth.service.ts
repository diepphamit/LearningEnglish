import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ACCESS_TOKEN, CURRENT_USER } from '../constants/db-keys';
import { Observable } from 'rxjs';
import { User } from '../models/user/user.model';
import { Token, LoginResponse } from '../models/user/login-response.model';

@Injectable()
export class AuthService {
    baseUrl = environment.apiUrl + 'auth/';
    decodedToken: Token;
    currentUser: User;
    loginRedirectUrl: string;
    logoutRedirectUrl: string;

    constructor(private http: HttpClient, public jwtHelper: JwtHelperService) {

    }

    login(model: any): Observable<any> {
        return this.http.post(this.baseUrl + 'login', model)
            .pipe(
                map((response: LoginResponse) => {
                    return this.processLoginResponse(response);
                })
            );
    }

    processLoginResponse(response: LoginResponse) {
        const token = response.access_token;
        const userResponse = response.user;

        if (token === null) {
            throw new Error('Received accessToken was empty');
        }
        this.decodedToken = this.jwtHelper.decodeToken(response.access_token) as Token;

        this.currentUser = new User(
            userResponse.id,
            userResponse.userName,
            userResponse.fullName,
            userResponse.email,
            Array.isArray(this.decodedToken.role) ? this.decodedToken.role : [this.decodedToken.role]);

        localStorage.setItem(ACCESS_TOKEN, token);
        localStorage.setItem(CURRENT_USER, JSON.stringify(this.currentUser));

        return this.currentUser;
    }

    logout() {
        localStorage.removeItem(ACCESS_TOKEN);
        localStorage.removeItem(CURRENT_USER);
    }

    get loggedIn() {
        const token = localStorage.getItem(ACCESS_TOKEN);
        return !this.jwtHelper.isTokenExpired(token);
    }
}
