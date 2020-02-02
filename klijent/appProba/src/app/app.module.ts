import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule,HTTP_INTERCEPTORS  } from '@angular/common/http';
import { AgmDirectionModule } from 'agm-direction';

import { FormsModule} from'@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';
import { TokenInterceptor } from 'src/app/interceptors/token.interceptor';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { Routes} from '@angular/router';
import { from } from 'rxjs';
import { RegistracijaComponent } from './registracija/registracija.component';
import { LoginComponent } from './auth/login/login.component';
import { LinijeComponent } from './linije/linije.component';
import { RedVoznjeComponent } from './red-voznje/red-voznje.component';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { InfoKorisnikComponent } from './info-korisnik/info-korisnik.component';
import { DodavanjeCenovnikaComponent } from './dodavanje-cenovnika/dodavanje-cenovnika.component';
import { LokacijaComponent } from 'src/app/lokacija/lokacija.component';
import { IzmenaCenovnikaComponent } from './izmena-cenovnika/izmena-cenovnika.component';
import { KarteComponent } from './karte/karte.component';
import { DodavanjeLinijeComponent } from './dodavanje-linije/dodavanje-linije.component';
import { BrisanjeLinijeComponent } from './brisanje-linije/brisanje-linije.component';
import { AdminGuard } from 'src/app/guards/admin.guard';
import { UserGuard } from 'src/app/guards/user.guard';
import { DodavanjeStaniceComponent } from './dodavanje-stanice/dodavanje-stanice.component';
import { KontrolerKorisniciComponent } from './kontroler-korisnici/kontroler-korisnici.component';
import { KontrolerKarteComponent } from './kontroler-karte/kontroler-karte.component';
import { ControllerGuard } from './guards/controller.guard';
import {LokacijaService} from 'src/app/servisi/lokacija.service';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
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
    DodavanjeStaniceComponent,
    KontrolerKorisniciComponent,
    KontrolerKarteComponent,
 
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AgmDirectionModule,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyDnihJyw_34z5S1KZXp90pfTGAqhFszNJk'})

  ],
  providers: [AdminGuard,UserGuard,ControllerGuard,{provide:HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
    DodavanjeCenovnikaComponent,IzmenaCenovnikaComponent,DodavanjeLinijeComponent,InfoKorisnikComponent,KarteComponent,BrisanjeLinijeComponent,DodavanjeStaniceComponent,LokacijaService],
  bootstrap: [AppComponent]
})
export class AppModule { }
