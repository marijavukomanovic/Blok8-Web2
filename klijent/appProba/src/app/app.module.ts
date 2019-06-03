import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import {Routes} from '@angular/router';
import { LogInComponent } from './log-in/log-in.component';

const routes : Routes =[
  {path:'',component:HomeComponent},
  {path: '/log-in',component:LogInComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LogInComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
