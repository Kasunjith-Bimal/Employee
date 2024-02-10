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
import { Employee } from '../models/Employee';
@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {
  
  private loggedInUserSubject: BehaviorSubject<LoginUser | null> = new BehaviorSubject<LoginUser | null>(null);

 constructor(private http: HttpClient) {
 }

 login(login: Login){
  const url = `${environment.baseUrl}api/Authentication/Login`;
  return this.http.post(url,login);
 }

 changePassword(changePassword: ChangePassword){
  debugger;
  const url = `${environment.baseUrl}api/Authentication/ChangePassword`;
  return this.http.put(url,changePassword);
 }

 register(register: Employee){
  debugger;
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


setLogedUser(){
  const token = this.getAccessToken();
  if (token !== null && token !== undefined && token !== ''){
    const helper = new JwtHelperService();
     var tokenDetail = JSON.parse(token);
     debugger;
     console.log(tokenDetail);
     let decodedToken = helper.decodeToken(tokenDetail.accessToken);
     console.log(decodedToken);
     const currentTime = Date.now() / 1000; // Convert milliseconds to seconds
     if(decodedToken.exp < currentTime){
      return null
     }else{
      let loginUser : LoginUser = {
        Email : decodedToken.email,
        FullName : decodedToken.name,
        IsFirstLogin :true
      };
      this.loggedInUserSubject.next(loginUser);
      return loginUser;
     }
  }else{
    return null;
  }
 
}


getLoggedInUser(): Observable<LoginUser | null> {
  debugger;
  return this.loggedInUserSubject.asObservable();
}


getAccessToken() {
  return localStorage.getItem('access-token');
}

logout(): void {
  //this.loggedInUserSubject.next(null);
  localStorage.removeItem('access-token');
  // Perform other logout actions if necessary
}

isTokenValid(): boolean {
  debugger;
  const token = this.getAccessToken();
  if (token !== null && token !== undefined && token !== ''){
    const helper = new JwtHelperService();
     var tokenDetail = JSON.parse(token);
     debugger;
     console.log(tokenDetail);
     let decodedToken = helper.decodeToken(tokenDetail.accessToken);
     console.log(decodedToken);
     const currentTime = Date.now() / 1000; // Convert milliseconds to seconds
     if(decodedToken.exp < currentTime){
      return false
     }else{
      return true;
     }
  }else{
    return false;
  }
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
