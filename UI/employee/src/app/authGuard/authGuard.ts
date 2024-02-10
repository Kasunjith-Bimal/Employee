import { ActivatedRouteSnapshot, CanActivate, Router } from "@angular/router";
import { AuthorizeService } from "../services/authorize.service";
import { Injectable } from "@angular/core";
@Injectable({
    providedIn: 'root',
  })
  export class AuthGuard implements CanActivate {
  
    constructor(private authorizeService:AuthorizeService , private router: Router) {}
  
    canActivate(route: ActivatedRouteSnapshot): boolean {
    
      if(this.authorizeService.isTokenValid()){
        let role = this.authorizeService.getRoleusingToken();
        const requiredRoles = route.data['roles'] as Array<string>;
        if (requiredRoles && !requiredRoles.includes(role)) {
          // Redirect to the default route based on user role if there's no access
          this.router.navigate([this.getDefaultRouteForRole(role)]);
          return false;
        }
        this.authorizeService.setLogedUser();
        return true;
      }else{
        this.router.navigate(['/authorize/login']);
        return false;
      }
    }

    getDefaultRouteForRole(role: string): string {
        switch (role) {
          case 'Admin':
            return '/admin/admins';
          case 'Employee':
            return '/employee/employeeDetail'; // Adjust this route as per your structure
          default:
            return '/authorize/login';
        }
      }
  }