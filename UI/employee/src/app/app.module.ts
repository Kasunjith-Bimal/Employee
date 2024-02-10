import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/authorize/login/login.component';
import { RegisterComponent } from './components/authorize/register/register.component';
import { ChangePasswordComponent } from './components/authorize/change-password/change-password.component';
import { AdminFormComponent } from './components/admin/admin-form/admin-form.component';
import { EmployeeFormComponent } from './components/employee/employee-form/employee-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EmployeeDetailComponent } from './components/employee/employee-detail/employee-detail.component';
import { AdminListComponent } from './components/admin/admin-list/admin-list.component';
import { AdminListItemComponent } from './components/admin/admin-list/admin-list-item/admin-list-item.component';
import { EmployeeListComponent } from './components/admin/employee-list/employee-list.component';
import { EmployeeListItemComponent } from './components/admin/employee-list/employee-list-item/employee-list-item.component';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationComponent } from './components/navigation/navigation/navigation.component';
import { LoaderComponent } from './components/loader/loader.component';


@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: false,
      closeButton: false,
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
