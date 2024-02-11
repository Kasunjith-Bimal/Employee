import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/models/Employee';
import { RoleType } from 'src/app/models/RoleType';
import { AuthorizeService } from 'src/app/services/authorize.service';
import { AdminService } from 'src/app/services/admin.service';

@Component({
  selector: 'app-admin-form',
  templateUrl: './admin-form.component.html',
  styleUrls: ['./admin-form.component.css']
})
export class AdminFormComponent implements OnInit {
  adminForm!: FormGroup;
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


  constructor(private formBuilder: FormBuilder,private authorizeService: AuthorizeService,private adminService : AdminService,private toastr : ToastrService,private router: Router,private route: ActivatedRoute) {


    this.adminForm = this.formBuilder.group({
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
    this.adminService.getAdminById(userId).subscribe(
      
      (response:any) => {
        if(response.succeeded){
          debugger;
        this.registerEmployee = response.payload.employee;
        console.log(this.registerEmployee)
        let joinDate : string =   response.payload.employee.joinDate.toString();
       
        this.adminForm.setValue({
          email: this.registerEmployee.email,
          fullName: this.registerEmployee.fullName,
          salary: this.registerEmployee.salary,
          joinDate: joinDate.split('T')[0],
          address:  this.registerEmployee.address,
          telephone : this.registerEmployee.telephone,
          role : this.registerEmployee.roleType == 1 ? "1":"2"
        });
        console.log( joinDate)
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
    debugger;
    if (this.adminForm.invalid) {
      return;
    }else{
      let registerValue = this.adminForm.value;
      debugger;
      let role = registerValue.role;
      console.log(role);

     let ediEmployee: Employee  = {
        id : this.userId,
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

      this.adminService.updateEmployeeByAdmin(ediEmployee).subscribe(
        (response: any) => {
          if(response.succeeded){
            this.toastr.success('Employee updated successfully', 'Success');
            setTimeout(() => {

              if(ediEmployee.roleType == RoleType.Admin){
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

    // console.log(this.adminForm.value); // Implement registration logic here
  }
}
