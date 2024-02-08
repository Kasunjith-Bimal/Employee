import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Employee } from './../models/Employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

   getAllEmployee(){
    const url = `${environment.baseUrl}api/Employee}`;
    return this.http.get(url);
   }

   getEmployeeById(employeeId: string){
    const url = `${environment.baseUrl}api/Employee/${employeeId}}`;
    return this.http.get(url);
   }

   updateEmployee(Employee:Employee){
    const url = `${environment.baseUrl}api/Employee/${Employee.Id}/edit}`;
    return this.http.put(url,Employee);
   }
}
