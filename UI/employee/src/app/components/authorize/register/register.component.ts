import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/models/Employee';
import { RoleType } from 'src/app/models/RoleType';
import { AuthorizeService } from 'src/app/services/authorize.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm!: FormGroup;
  registerEmployee : Employee  = {
    id : "",
    email :'',
    firstName :'',
    lastName: '',
    fullName :'',
    salary : 0,
    joinDate : new Date(),
    telephone : "",
    address :"",
    roleType : RoleType.Employee

  }
  constructor(private formBuilder: FormBuilder,private authorizeService: AuthorizeService,private toastr : ToastrService,private router: Router) {


    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      fullName: ['', Validators.required],
      salary: ['', [Validators.required, Validators.pattern(/^[0-9]+(\.[0-9]{1,2})?$/)]],
      joinDate: ['', Validators.required],
      address :['', Validators.required],
      telephone: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
      role: ['', Validators.required]
    });
  }

  ngOnInit() {
    let joinDate =  new Date(this.registerEmployee.joinDate);
    this.registerForm.setValue({
      email: this.registerEmployee.email,
      fullName: this.registerEmployee.fullName,
      salary: this.registerEmployee.salary,
      joinDate: joinDate.toISOString().split('T')[0],
      address:  this.registerEmployee.address,
      telephone : this.registerEmployee.telephone,
      role : RoleType.Employee
    });
  }

  onSubmit() {
    debugger;
    if (this.registerForm.invalid) {
      return;
    }else{
      let registerValue = this.registerForm.value;
      debugger;
      let role = registerValue.role;
      console.log(role);

     let newEmployee: Employee  = {
        id : "",
        email :registerValue.email,
        firstName :'',
        lastName: '',
        fullName :registerValue.fullName,
        salary : registerValue.salary,
        joinDate : registerValue.joinDate,
        telephone : registerValue.telephone,
        address :registerValue.address,
        roleType : role == "1" ? RoleType.Admin : RoleType.Employee
    
      }
      this.authorizeService.register(newEmployee).subscribe(
        (response: any) => {
          if(response.succeeded){
            this.toastr.success('Employee registed successfully', 'Success');
            setTimeout(() => {

              if(registerValue.role == 1){
                this.router.navigate(['admin/admins']);
              }else{
                this.router.navigate(['admin/employees']); 
              }
            }, 500);
          }else{
            this.toastr.error(response.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
          }
         
        },
        error => {
          this.toastr.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
        }
      );
    }

    console.log(this.registerForm.value); // Implement registration logic here
  }
}
