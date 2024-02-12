import { AfterViewInit, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { LoginUser } from 'src/app/models/LoginUser';
import { AuthorizeService } from 'src/app/services/authorize.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit,OnDestroy {
 isLoggedIn: boolean = false;
 currentLogUser: LoginUser | null = null;
 private currentLogUserSubscription: Subscription = new Subscription();
 role : string ="";
 activeLink: string = '';

 constructor(private authorizeService : AuthorizeService,private router: Router) {
  this.isLoggedIn = false;
 }
 ngOnDestroy(): void {
  this.currentLogUserSubscription.unsubscribe();
 }

 setActiveLink(link: string) {
  this.activeLink = link;
  this.router.navigate([link]);
}
 
 
  ngOnInit(): void {
    
    if(this.authorizeService.isTokenValid()){
    
      //console.log("Role",this.role);
      this.currentLogUserSubscription = this.authorizeService.getLoggedInUser().subscribe((user) => {
        this.currentLogUser = user;
        this.role = this.authorizeService.getRoleusingToken();
        
        if(this.role == "Admin"){
          if( this.activeLink == ''){
            this.activeLink = 'admin/admins';
          }
        }else{
          if( this.activeLink == ''){
            this.activeLink = 'employee/employeeDetail';
          }
         
        }
        if(this.currentLogUser != null){
          this.isLoggedIn = true; 
        }
        
      });
    }
  }


  logout() {
    this.isLoggedIn = false;
    this.authorizeService.logout();
    this.router.navigate(['authorize/login']);
 
  }

  changePassword(){
    this.router.navigate(['authorize/changepassword']);
  }
}
