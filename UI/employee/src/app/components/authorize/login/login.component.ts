import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Login } from './../../../models/Login';
import { AuthorizeService } from './../../../services/authorize.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  implements OnInit{
  loadingForm : boolean = false;
  loginForm!: FormGroup;
  isErrorShowing : boolean = false;
  errorMessage : string = '';
  constructor(private formBuilder: FormBuilder,private authorizeService : AuthorizeService,private toastr: ToastrService,private router: Router) {}

  ngOnInit() {
    if(this.authorizeService.isTokenValid()){
      let role = this.authorizeService.getRoleusingToken();
      if(role == "Admin"){
        this.router.navigate(['admin/admins']);
      }else{
        //employee Role 
        this.router.navigate(['employee/employeeDetail']);
      }
    }else{
      this.loadingForm = true;
      this.loginForm = this.formBuilder.group({
        username: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required]
      });
    }
   
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      // console.log("Login Fail")
      this.toastr.error("Login Fail");
    }else{
      
      let login : Login =
      {
       Email : this.loginForm.value.username,
       Password : this.loginForm.value.password
      } 
      //console.log(login);
      this.authorizeService.login(login).subscribe((response:any)=>{
        if(response.succeeded){
          debugger;
          this.toastr.success("Login Success");
          console.log(response);
          if(response.payload.isFirstLogin){
            this.authorizeService.setAccessTokenAndUser(response.payload);
            this.router.navigate(['authorize/changepassword']);
          }else{
            this.authorizeService.setAccessTokenAndUser(response.payload);
            setTimeout(() => {
              let role = this.authorizeService.getRoleusingToken();
              if(role == "Admin"){
                this.router.navigate(['admin/admins']);
              }else{
                //employee Role 
                this.router.navigate(['employee/employeeDetail']);
              }
            }, 500);
           
          }
         
          // navigation  route be use;
        }else{
          console.log("re",response)
          this.isErrorShowing = true;
          this.errorMessage = response.message;
          setTimeout(() => {
            this.isErrorShowing = false;
          }, 3000);

        }
      },
      error => {
        console.log(error)
        this.isErrorShowing = true;
          this.errorMessage = error.error.message;
          setTimeout(() => {
            this.isErrorShowing = false;
          }, 3000);
      });
    }

    // Implement login logic here
   
  }
}
