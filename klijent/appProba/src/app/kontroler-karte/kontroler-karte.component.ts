import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { KontrolerService } from '../servisi/kontroler.service';
import { Router } from '@angular/router';
import { RegistracijaServis } from '../servisi/registracija.servis';

@Component({
  selector: 'app-kontroler-karte',
  templateUrl: './kontroler-karte.component.html',
  styleUrls: ['./kontroler-karte.component.css']
})
export class KontrolerKarteComponent implements OnInit {

  username:string;
  role:string;
  message:string;
  korisnici : Array<string>;

  constructor(private fb : FormBuilder, private kontrolerServis : KontrolerService, private router:Router,private userService : RegistracijaServis) { }

  ngOnInit() {
    
    this.username = localStorage.getItem('username');
    this.role = localStorage.getItem('role');
    this.message = "";

    this.kontrolerServis.getId().subscribe(data => {
      console.log(data);
      this.korisnici = data;
      
    });

  }
  getKorisnik(id:string)
  {
    if(id=="")
    {
      id = "lose";
    }
    this.kontrolerServis.getVer(id).subscribe(data => {
      console.log(data);
      this.message = data;
      
    });
  }


}
