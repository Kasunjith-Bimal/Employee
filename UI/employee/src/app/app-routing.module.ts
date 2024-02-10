import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './authGuard/authGuard';
const routes: Routes = [
  { path: 'authorize', 
    loadChildren: () => import('./components/authorize/authorize.module').then(m => m.AuthorizeModule) 
  },
  {
    path: 'admin',
    loadChildren: () => import('./components/admin/admin.module').then(m => m.AdminModule),
    canActivate: [AuthGuard],
    data: { roles: ['Admin'] },
  },
  { path: 'employee', 
    loadChildren: () => import('./components/employee/employee.module').then(m => m.EmployeeModule),
    canActivate: [AuthGuard],
    data: { roles: ['Employee'] },
  },
  { path: '', redirectTo: '/authorize/login', pathMatch: 'full' },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
