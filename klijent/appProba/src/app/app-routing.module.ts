import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LogInComponent} from './log-in/log-in.component';
import { HomeComponent } from './home/home.component';
import {RegistracijaComponent} from './registracija/registracija.component';
//
import { AuthGuard } from './auth/auth.guard';
import { LoginComponent } from './auth/login/login.component';

const routes: Routes = [
  {path: '',component: HomeComponent},
  {path:'log-in',component:LogInComponent},
  {path:'registracija',component:RegistracijaComponent},
  { 
    path: 'login', 
    component: LoginComponent, 
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
