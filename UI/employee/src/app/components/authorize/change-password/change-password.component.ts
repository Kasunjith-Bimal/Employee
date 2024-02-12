import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthorizeService } from './../../../services/authorize.service';
import { ToastrService } from 'ngx-toastr';
import { ChangePassword } from 'src/app/models/ChangePassword';
import { Login } from 'src/app/models/Login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm!: FormGroup;
  isErrorShowing: boolean = false;
  errorMessage: any;

  constructor(private formBuilder: FormBuilder,private authorizeService: AuthorizeService,private toastr: ToastrService,private router: Router) {}

  ngOnInit() {
    this.changePasswordForm = this.formBuilder.group({
      email :['', [Validators.required, Validators.email]],
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmNewPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(frm: FormGroup) {
    return frm.controls['newPassword'].value === frm.controls['confirmNewPassword'].value ? null : { 'passwordMismatch': true };
  }

  loginAfterPasswordChange(login : Login){
    this.authorizeService.login(login).subscribe((response:any)=>{
      if(response.succeeded){
        //console.log(response);
        if(response.payload.isFirstLogin){
          this.authorizeService.setAccessTokenAndUser(response.payload);
          this.router.navigate(['authorize/changepassword']);
        }else{
          this.authorizeService.setAccessTokenAndUser(response.payload);
          let role = this.authorizeService.getRoleusingToken();
          if(role == "Admin"){
            this.router.navigate(['admin/admins']);
          }else{
            //employee Role 
            this.router.navigate(['employee/employeeDetail']);
          }
        }
       
        // navigation  route be use;
      }else{
       // console.log("re",response)
        this.isErrorShowing = true;
        this.errorMessage = response.message;
        setTimeout(() => {
          this.isErrorShowing = false;
        }, 3000);

      }
    },
    error => {
     // console.log(error)
      this.isErrorShowing = true;
        this.errorMessage = error.error.message;
        setTimeout(() => {
          this.isErrorShowing = false;
        }, 3000);
    });
  }

  onSubmit() {
    if (this.changePasswordForm.invalid) {
      return;
    }else{
      
      let changePassword :ChangePassword  =
      {
       Email : this.changePasswordForm.value.email,
       OldPassword : this.changePasswordForm.value.currentPassword,
       NewPassword : this.changePasswordForm.value.newPassword
      } 
      //console.log(login);
      this.authorizeService.changePassword(changePassword).subscribe((response:any)=>{
        if(response.succeeded){
          this.toastr.success("Password Change Success");
          
          let Newlogin : Login =
          {
           Email : changePassword.Email,
           Password : changePassword.NewPassword
          }
          
          this.loginAfterPasswordChange(Newlogin);
          
        }else{
          //console.log("re",response)
          this.isErrorShowing = true;
          this.errorMessage = response.message;
          setTimeout(() => {
            this.isErrorShowing = false;
          }, 3000);

        }
      },
      error => {
       // console.log(error)
        this.isErrorShowing = true;
          this.errorMessage = error.error.message;
          setTimeout(() => {
            this.isErrorShowing = false;
          }, 3000);
      });
    }

    // Implement change password logic
  }
}
