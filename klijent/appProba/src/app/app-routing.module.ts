import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LogInComponent} from './log-in/log-in.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '',component: HomeComponent},
  {path:'log-in',component:LogInComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
