import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LogInComponent} from './log-in/log-in.component';
import { HomeComponent } from './home/home.component';
import {RegistracijaComponent} from './registracija/registracija.component';
import {LinijeComponent} from './linije/linije.component';
import {RedVoznjeComponent} from './red-voznje/red-voznje.component';
import {CenovnikComponent} from './cenovnik/cenovnik.component';
import {InfoKorisnikComponent} from './info-korisnik/info-korisnik.component';
import {KarteComponent} from './karte/karte.component';
//
import { AuthGuard } from './auth/auth.guard';
import { LoginComponent } from './auth/login/login.component';
import {DodavanjeCenovnikaComponent} from './dodavanje-cenovnika/dodavanje-cenovnika.component';
import {LokacijaComponent} from './lokacija/lokacija.component';
import {IzmenaCenovnikaComponent} from './izmena-cenovnika/izmena-cenovnika.component';
import {DodavanjeLinijeComponent} from './dodavanje-linije/dodavanje-linije.component';
import {BrisanjeLinijeComponent} from './brisanje-linije/brisanje-linije.component';
import{AdminGuard } from 'src/app/guards/admin.guard';
import{UserGuard } from 'src/app/guards/user.guard';


const routes: Routes = [
  {path: '',component: HomeComponent},
  {path:'log-in',component:LogInComponent},
  {path:'registracija',component:RegistracijaComponent},
  { path: 'login', component: LoginComponent },
  {path: 'linije',component:LinijeComponent},
  {path: 'red-voznje',component: RedVoznjeComponent},
  {path: 'cenovnik',component:CenovnikComponent},
  {path: 'info',component: InfoKorisnikComponent,canActivate:[UserGuard]},
  {path:'dodavanjeCenovnika',component:DodavanjeCenovnikaComponent,canActivate:[AdminGuard]},
  {path:'lokacija',component : LokacijaComponent},
  {path:'izmenaCenovnika',component:IzmenaCenovnikaComponent,canActivate:[AdminGuard]},
  {path:'karteKorisnik',component:KarteComponent,canActivate:[UserGuard]},
  {path: 'dodavanjeLinije',component:DodavanjeLinijeComponent,canActivate:[AdminGuard]},
  {path:'brisanjeLinije',component:BrisanjeLinijeComponent,canActivate:[AdminGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
