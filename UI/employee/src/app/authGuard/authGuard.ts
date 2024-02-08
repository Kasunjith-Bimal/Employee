import { CanActivate, Router } from "@angular/router";
import { AuthorizeService } from "../services/authorize.service";
import { Injectable } from "@angular/core";
@Injectable({
    providedIn: 'root',
  })
  export class AuthGuard implements CanActivate {
  
    constructor(private authorizeService:AuthorizeService , private router: Router) {}
  
    canActivate(): boolean {
      if(this.authorizeService.isTokenValid()){
        return true;
      }else{
        this.router.navigate(['/login']);
        return false;
      }
    }
  }