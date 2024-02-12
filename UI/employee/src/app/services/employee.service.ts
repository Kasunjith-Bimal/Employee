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
    const url = `${environment.baseUrl}api/Employe`;
    return this.http.get(url);
   }

   getEmployeeById(employeeId: string){
    const url = `${environment.baseUrl}api/Employe/${employeeId}`;
    return this.http.get(url);
   }

   updateEmployee(Employee:Employee){
    const url = `${environment.baseUrl}api/Employe/${Employee.id}/edit`;
    return this.http.put(url,Employee);
   }
}
