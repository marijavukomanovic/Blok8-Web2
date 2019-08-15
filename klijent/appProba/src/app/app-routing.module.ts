import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LogInComponent} from './log-in/log-in.component';
import { HomeComponent } from './home/home.component';
import {RegistracijaComponent} from './registracija/registracija.component';

const routes: Routes = [
  {path: '',component: HomeComponent},
  {path:'log-in',component:LogInComponent},
  {path:'registracija',component:RegistracijaComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
