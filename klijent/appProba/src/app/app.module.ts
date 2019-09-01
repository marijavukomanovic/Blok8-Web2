import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule,HTTP_INTERCEPTORS  } from '@angular/common/http';

import {FormsModule} from'@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';
import { JwtInterceptor } from 'src/app/auth/jwt-interceptor';

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
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { InfoKorisnikComponent } from './info-korisnik/info-korisnik.component';
import { DodavanjeCenovnikaComponent } from './dodavanje-cenovnika/dodavanje-cenovnika.component';
import { LokacijaComponent } from './lokacija/lokacija.component';
import { IzmenaCenovnikaComponent } from './izmena-cenovnika/izmena-cenovnika.component';
import { KarteComponent } from './karte/karte.component';
import { DodavanjeLinijeComponent } from './dodavanje-linije/dodavanje-linije.component';
import { BrisanjeLinijeComponent } from './brisanje-linije/brisanje-linije.component';
import{AdminGuard } from 'src/app/guards/admin.guard';
import{UserGuard } from 'src/app/guards/user.guard';

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
    CenovnikComponent,
    InfoKorisnikComponent,
    DodavanjeCenovnikaComponent,
    LokacijaComponent,
    IzmenaCenovnikaComponent,
    KarteComponent,
    DodavanjeLinijeComponent,
    BrisanjeLinijeComponent,
 
    
    //RegistracijaServis,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyDnihJyw_34z5S1KZXp90pfTGAqhFszNJk'})

  ],
  providers: [AdminGuard,UserGuard,{provide:HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    DodavanjeCenovnikaComponent,DodavanjeLinijeComponent,InfoKorisnikComponent,IzmenaCenovnikaComponent,KarteComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
