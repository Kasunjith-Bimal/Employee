import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Login } from './../models/Login';
import { environment } from 'src/environments/environment';
import { ChangePassword } from '../models/ChangePassword';
import { Register } from '../models/Register';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginUserAndAccessToken } from '../models/LoginUserAndAccessToken';
import { LoginUser } from '../models/LoginUser';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Token } from '@angular/compiler';
@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {
  
 private loggedInUserSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
 constructor(private http: HttpClient) {
 }

 login(login: Login){
  const url = `${environment.baseUrl}api/Authentication/Login`;
  return this.http.post(url,login);
 }

 changePassword(changePassword: ChangePassword){
  const url = `${environment.baseUrl}api/Authentication/ChangePassword`;
  return this.http.put(url,changePassword);
 }

 register(register: Register){
  const url = `${environment.baseUrl}api/Authentication/Register`;
  return this.http.post(url,register);
 }


setAccessTokenAndUser(response: any): void {
  debugger;
  localStorage.setItem('access-token', JSON.stringify(response.tokenDetail));
  let LoginUser : LoginUser = {
    Email : response.email,
    FullName : response.fullName,
    IsFirstLogin :response.isFirstLogin
  };
  this.loggedInUserSubject.next(LoginUser);
}

getLoggedInUser(): Observable<any> {
  return this.loggedInUserSubject.asObservable();
}


getAccessToken() {
  return localStorage.getItem('access-token');
}

logout(): void {
  this.loggedInUserSubject.next(null);
  localStorage.removeItem('access-token');
  // Perform other logout actions if necessary
}

isTokenValid(): boolean {
  const token = this.getAccessToken();
  return token !== null && token !== undefined && token !== '';
}


getRoleusingToken() {
  debugger;
  const token = this.getAccessToken();
  if (token !== null && token !== undefined && token !== ''){
    const helper = new JwtHelperService();
     var tokenDetail = JSON.parse(token);
     debugger;
     console.log(tokenDetail);
    let decodedToken = helper.decodeToken(tokenDetail.accessToken);
    console.log(decodedToken);
    return decodedToken.role;

  }
}
}
