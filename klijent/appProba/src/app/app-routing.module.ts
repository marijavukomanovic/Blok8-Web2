import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LogInComponent} from './log-in/log-in.component';
import { HomeComponent } from './home/home.component';
import {RegistracijaComponent} from './registracija/registracija.component';
import {LinijeComponent} from './linije/linije.component';
import {RedVoznjeComponent} from './red-voznje/red-voznje.component';
import {CenovnikComponent} from './cenovnik/cenovnik.component';
import {InfoKorisnikComponent} from './info-korisnik/info-korisnik.component';
//
import { AuthGuard } from './auth/auth.guard';
import { LoginComponent } from './auth/login/login.component';
import {DodavanjeCenovnikaComponent} from './dodavanje-cenovnika/dodavanje-cenovnika.component';

const routes: Routes = [
  {path: '',component: HomeComponent},
  {path:'log-in',component:LogInComponent},
  {path:'registracija',component:RegistracijaComponent},
  { path: 'login', component: LoginComponent },
  {path: 'linije',component:LinijeComponent},
  {path: 'red-voznje',component: RedVoznjeComponent},
  {path: 'cenovnik',component:CenovnikComponent},
  {path: 'info',component: InfoKorisnikComponent},
  {path:'dodavanjeCenovnika',component:DodavanjeCenovnikaComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
