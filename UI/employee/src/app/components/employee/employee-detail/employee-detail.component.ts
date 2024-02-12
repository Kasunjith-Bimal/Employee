import { Component, OnDestroy, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { LoginUser } from 'src/app/models/LoginUser';
import { AuthorizeService } from 'src/app/services/authorize.service';
import { EmployeeService } from 'src/app/services/employee.service';
import { Employee } from 'src/app/models/Employee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit,OnDestroy {

  constructor(private authorizeService: AuthorizeService,private employeeService : EmployeeService,private toaster: ToastrService,private router: Router) {
  }
  currentLogUser: LoginUser | null = null;
  employee : Employee | null = null;
  private currentLogUserSubscription: Subscription = new Subscription();
  role: string ="";
  isLoading : boolean = false;
  ngOnDestroy(): void {
    this.currentLogUserSubscription.unsubscribe();
  }
  ngOnInit(): void {
    this.isLoading = true;
    this.currentLogUserSubscription = this.authorizeService.getLoggedInUser().subscribe((user) => {
      this.currentLogUser = user;
      console.log(this.currentLogUser);
      if(this.currentLogUser){
        this.role = this.authorizeService.getRoleusingToken();
        this.employeeService.getEmployeeById(this.currentLogUser.Id).subscribe(
          (response: any) => {
            if(response.succeeded){
             this.employee = response.payload.employee;
             this.isLoading = false;
            }else{
              this.isLoading = false;
              this.toaster.error(response.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
            }
           
          },
          error => {
            this.isLoading = false;
            this.toaster.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
          }
          );
      }
      
    });
  }

  onEdit(id : string){
    this.router.navigate(['/employee/'+id+'/edit'])
  }

}
