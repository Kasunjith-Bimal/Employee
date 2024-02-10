import { Component, Input } from '@angular/core';
import { Employee } from 'src/app/models/Employee';

@Component({
  selector: 'app-employee-list-item',
  templateUrl: './employee-list-item.component.html',
  styleUrls: ['./employee-list-item.component.css']
})
export class EmployeeListItemComponent {
  @Input() item!: Employee;

  onDelete(id:string){

  }
}
