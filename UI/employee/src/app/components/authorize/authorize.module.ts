import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

import { BrowserModule } from '@angular/platform-browser';
import { AuthGuard } from 'src/app/authGuard/authGuard';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { 
    path: 'register', 
    component: RegisterComponent, 
     canActivate: [AuthGuard],
     data: { roles: ['Admin'] } },
  { 
    path: 'changepassword', 
    component: ChangePasswordComponent,
  },
];


@NgModule({
  declarations: [
    LoginComponent,
    ChangePasswordComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
  ]
})
export class AuthorizeModule { }
