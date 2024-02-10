import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/models/Employee';
import { AdminService } from 'src/app/services/admin.service';
import { EmployeeService } from 'src/app/services/employee.service';


@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent {
  items : Employee[] = [];
  searchText : string ="";
  isLoading: boolean = false;
  constructor(private employeeService: EmployeeService, private toaster : ToastrService) {
   
    
  }
  ngOnInit(): void {
    this.isLoading = true;
    this.employeeService.getAllEmployee().subscribe((response:any)=>{
     
      if(response.succeeded){
      this.items = response.payload.employees;
      this.isLoading = false;
      }else{
       this.items = [];
       this.toaster.error(response.message, 'System Error. Please contact administrator',{timeOut: 2000,extendedTimeOut: 0});
       this.isLoading = false;
      }
      },
      error => {
       this.items = [];
       this.toaster.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 2000,extendedTimeOut: 0});
       this.isLoading = false;
     })
  }

  deleteItem(id:string){

  }
}
