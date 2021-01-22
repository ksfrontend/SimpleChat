import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard, AuthGuardLoggedIn } from './core/authentication';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { SiteLayoutComponent } from './_layout/site-layout/site-layout.component';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
    canActivate: [AuthGuardLoggedIn]
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [AuthGuardLoggedIn]
  },
  {
    path: 'dashboard',
    component: SiteLayoutComponent,
    children: [
      {
        path: '',
        component: DashboardComponent,
      }
    ],
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
