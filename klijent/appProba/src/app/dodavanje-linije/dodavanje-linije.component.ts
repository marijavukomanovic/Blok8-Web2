import { Component, OnInit } from '@angular/core';
import { Line } from '../model/linijaModel';
import { FormBuilder, Validators } from '@angular/forms';
import {LinijaService} from 'src/app/servisi/linija.service';
import {RegistracijaServis} from 'src/app/servisi/registracija.servis';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dodavanje-linije',
  templateUrl: './dodavanje-linije.component.html',
  styleUrls: ['./dodavanje-linije.component.css']
})
export class DodavanjeLinijeComponent implements OnInit {

 message:string;
  username:string;
  role:string;
  bojes :Array<string>;

  linija : Line = {
    LineType: 0,
    LineId: '',
    Description: '',
    Color: '',
  };

  
  linijaForma = this.fb.group(
    {
      LineType : [''],
      LineId:['',Validators['[0-9]*[a-bA-B]+']],
      Description :[''],
      Color:[''],
    }
  );

  constructor(private fb: FormBuilder,private linijaServis:LinijaService,private userService:RegistracijaServis,private router:Router) { }

  ngOnInit() {
    this.username = localStorage.getItem('username');
    this.role=localStorage.getItem('role');
    this.bojes = ["green","blue","yellow","black","pink","red","orange"];
  }

  onSubmit()
  {
    if( this.linijaForma.controls['LineId'].value=="" || this.linijaForma.controls['Description'].value==""){
      this.message = "Morate popuniti sva polja!";
    }
    else if( this.linijaForma.controls['LineType'].value==0 || this.linijaForma.controls['Color'].value==""){
      this.message = "Morate popuniti sva polja!";
    }
    else
    {
      this.linija = this.linijaForma.value;
    console.log(this.linija);
    this.message="";
    this.linijaServis.napraviLiniju(this.linija).subscribe(data => {
      console.log('Uspesno napravljena linija!');
    });
    }
    
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
