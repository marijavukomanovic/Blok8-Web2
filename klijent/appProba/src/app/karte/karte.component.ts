import { Component, OnInit } from '@angular/core';
import {RegistracijaServis} from 'src/app/servisi/registracija.servis';
import {Router} from '@angular/router';
import {KartaService} from 'src/app/servisi/karta.service';
import { KartaModel } from '../model/karteModel';

@Component({
  selector: 'app-karte',
  templateUrl: './karte.component.html',
  styleUrls: ['./karte.component.css']
})
export class KarteComponent implements OnInit {
  username: string;
  role:string;
  lista : Array<KartaModel>;

  constructor(private userService: RegistracijaServis,private router:Router,private kartaServis : KartaService) { }

  ngOnInit() {
    this.username = localStorage.getItem('username');
    this.role = localStorage.getItem('role');
    this.kartaServis.getKarte(this.username).subscribe(data => {
      console.log('Uspesno stigle karte!!');
      this.lista = data;
    });
  }

  onClickLogout(event: Event): void {
    event.preventDefault(); // Prevents browser following the link
    // Here you can call your service method to logout the user
    // and then redirect with Router object, for example
    this.username = '';
    this.role = '';
    this.userService.logout();
    this.router.navigate(['login']);
  }
}
