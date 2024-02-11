import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from 'src/app/models/Employee';

@Component({
  selector: 'app-employee-list-item',
  templateUrl: './employee-list-item.component.html',
  styleUrls: ['./employee-list-item.component.css']
})
export class EmployeeListItemComponent {

  constructor(private router: Router) {
  }
  @Input() item!: Employee;
  @Output() delete = new EventEmitter<string>(); 
  onDelete(id:string){
    this.delete.emit(id);
  }

  onEdit(id:string){
    this.router.navigate(['/admin/'+id+'/edit'])
  }
}
