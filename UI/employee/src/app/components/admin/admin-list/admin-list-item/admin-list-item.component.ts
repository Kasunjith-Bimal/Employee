import { Component, Input } from '@angular/core';
import { Employee } from 'src/app/models/Employee';

@Component({
  selector: 'app-admin-list-item',
  templateUrl: './admin-list-item.component.html',
  styleUrls: ['./admin-list-item.component.css']
})
export class AdminListItemComponent {
  @Input() item!: Employee;

  onDelete(id:string){

  }
}
