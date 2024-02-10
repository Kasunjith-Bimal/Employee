import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/models/Employee';
import { AdminService } from 'src/app/services/admin.service';


@Component({
  selector: 'app-admin-list',
  templateUrl: './admin-list.component.html',
  styleUrls: ['./admin-list.component.css']
})
export class AdminListComponent implements OnInit {
  items : Employee[] = [];
  searchText : string ="";
  isLoading : boolean = false;
  constructor(private adminService: AdminService,private toaster: ToastrService) {
   
    
  }
  ngOnInit(): void {
    this.isLoading = true;
     this.adminService.getAllAdmins().subscribe((response:any)=>{
     
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
