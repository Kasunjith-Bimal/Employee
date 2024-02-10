import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Employee } from '../models/Employee';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

   getAllAdmins(){
    const url = `${environment.baseUrl}api/Admin`;
    return this.http.get(url);
   }

   getAdminById(adminId:string){
    const url = `${environment.baseUrl}api/Admin/${adminId}}`;
    return this.http.get(url);
   }

   deleteEmployeeByAdmin(employeeId:string){
    const url = `${environment.baseUrl}api/Admin/Employee/${employeeId}/delete}`;
    return this.http.delete(url);
   }


   updateEmployeeByAdmin(employee: Employee){
    const url = `${environment.baseUrl}api/Admin/Employee/${employee.id}/edit}`;
    return this.http.put(url,employee);
   }
}
