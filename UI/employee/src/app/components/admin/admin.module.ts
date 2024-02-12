import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminListComponent } from './admin-list/admin-list.component';
import { AdminListItemComponent } from './admin-list/admin-list-item/admin-list-item.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeListItemComponent } from './employee-list/employee-list-item/employee-list-item.component';
import { AdminFormComponent } from './admin-form/admin-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/authGuard/authGuard';
import { SearchPipe } from 'src/app/pipes/search.pipe';
import { LoaderComponent } from '../loader/loader.component';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';


const routes: Routes = [
  { path: 'admins', component: AdminListComponent },
  { path: 'employees', component: EmployeeListComponent },
  { path: ':id/edit', component: AdminFormComponent },
];

@NgModule({
  declarations: [
    SearchPipe,
    AdminListComponent,
    AdminListItemComponent,
    EmployeeListComponent,
    EmployeeListItemComponent,
    AdminFormComponent,
    ConfirmationDialogComponent,
    LoaderComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
  ]
})
export class AdminModule { }
