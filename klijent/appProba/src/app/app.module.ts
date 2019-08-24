import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import {FormsModule} from'@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import {Routes} from '@angular/router';
import { LogInComponent } from './log-in/log-in.component';
import { from } from 'rxjs';
import { RegistracijaComponent } from './registracija/registracija.component';
import { LoginComponent } from './auth/login/login.component';
import { LinijeComponent } from './linije/linije.component';
import { RedVoznjeComponent } from './red-voznje/red-voznje.component';
//import {RegistracijaServis} from './servisi/registracija.servis';


//const routes : Routes =[
  //{path:'',component:HomeComponent},
  //{path: '/log-in',component:LogInComponent},
 // {path: '/registracija',component:RegistracijaComponent},
//];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LogInComponent,
    RegistracijaComponent,
    LoginComponent,
    LinijeComponent,
    RedVoznjeComponent,
    //RegistracijaServis,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
