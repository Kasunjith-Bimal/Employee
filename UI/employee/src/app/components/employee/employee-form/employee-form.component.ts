import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/models/Employee';
import { RoleType } from 'src/app/models/RoleType';
import { AuthorizeService } from 'src/app/services/authorize.service';
import { EmployeeService } from 'src/app/services/employee.service';
import { LoginUser } from 'src/app/models/LoginUser';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements  OnInit {
  employeeForm!: FormGroup;
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
  isLoading: boolean = false;
  userId: string="";


  constructor(private formBuilder: FormBuilder,private authorizeService: AuthorizeService,private employeeService : EmployeeService,private toastr : ToastrService,private router: Router,private route: ActivatedRoute) {


    this.employeeForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      address :['', Validators.required],
      telephone: ['', [Validators.required]],
    });
  }

  ngOnInit() {

    this.route.params.subscribe(params => {
      if (params['id']) {
        this.isLoading = true;
        this.userId = (params['id']);
        this.loadUserdetails( this.userId );
      }
    });
    // let joinDate =  new Date(this.registerEmployee.joinDate);
    
  }

  loadUserdetails(userId :string){
    this.employeeService.getEmployeeById(userId).subscribe(
      
      (response:any) => {
        if(response.succeeded){
        this.registerEmployee = response.payload.employee;
      
        this.employeeForm.setValue({
          fullName: this.registerEmployee.fullName,
          address:  this.registerEmployee.address,
          telephone : this.registerEmployee.telephone,
        });
       
        this.isLoading = false;
        }else{
          this.isLoading = false;
          this.toastr.error(response.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
          setTimeout(() => {
            //this.router.navigate(['tasks']);
          }, 500);
          
        }
      },
      error => {
        this.isLoading = false;
         this.toastr.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
         setTimeout(() => {
          //this.router.navigate(['tasks']);
         }, 500);
      }
     );
  }

  onSubmit() {

    if (this.employeeForm.invalid) {
      return;
    }else{
      let registerValue = this.employeeForm.value;
     
     let ediEmployee: Employee  = {
        id : this.userId,
        email :'',
        firstName :'',
        lastName: '',
        fullName :registerValue.fullName,
        salary : 0,
        joinDate : new Date(),
        telephone : registerValue.telephone,
        address :registerValue.address,
        roleType :  RoleType.Employee
    
      }

      this.employeeService.updateEmployee(ediEmployee).subscribe(
        (response: any) => {
          if(response.succeeded){
            let loginUser : LoginUser = {
             Email :"",
             FullName :registerValue.fullName,
             Id :this.userId,
             IsFirstLogin : false
            }; 
            this.authorizeService.updateUserToken(loginUser);
            this.toastr.success('Employee updated successfully', 'Success');
            setTimeout(() => {
             this.router.navigate(['admin/employees']); 
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

    // console.log(this.employeeForm.value); // Implement registration logic here
  }
}
