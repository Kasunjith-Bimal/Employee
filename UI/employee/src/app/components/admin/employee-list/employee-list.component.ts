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
  showConfirmation : boolean = false;
  deleteId : string = "";
  constructor(private employeeService: EmployeeService,private adminService: AdminService, private toaster : ToastrService) {
   
    
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

  onConfirmDelete(){
    this.showConfirmation = false;
    this.adminService.deleteEmployeeByAdmin(this.deleteId).subscribe((response:any)=>{
     
      if(response.succeeded){
        this.items = this.items.filter(x=>x.id != this.deleteId);
        this.toaster.success("Employee Delete Successfuly", 'Success');
       
      }else{
       this.toaster.error(response.message, 'System Error. Please contact administrator',{timeOut: 2000,extendedTimeOut: 0});
      }
      },
      error => {
       this.toaster.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 2000,extendedTimeOut: 0});
     });
  }

  onCancelDelete(){
    this.showConfirmation = false;
  }

  deleteItem(id:string){
    this.deleteId = id;
    this.showConfirmation = true;
  }
}
