import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import {AdminService} from 'src/app/servisi/admin.service';
import { CenovnikModel } from '../model/cenovnikModel';
import { Router } from '@angular/router';
import { RegistracijaServis } from '../servisi/registracija.servis';

@Component({
  selector: 'app-izmena-cenovnika',
  templateUrl: './izmena-cenovnika.component.html',
  styleUrls: ['./izmena-cenovnika.component.css']
})
export class IzmenaCenovnikaComponent implements OnInit {

  info:string;
  username:string;
  role:string;

cenovnik : CenovnikModel = {
  OD : '',
  DO : '',
  cenaVremenska : 0,
  cenaDnevna : 0,
  cenaMesecna : 0,
  cenaGodisnja : 0,
};

cenovnikForma = this.fb.group(
  {
    OD : [''],
    DO:[''],
    cenaVremenska :[''],
    cenaDnevna:[''],
    cenaMesecna:[''],
    cenaGodisnja:['']
  }
);

  constructor(private adminServis : AdminService,private fb: FormBuilder,private router:Router,private userService:RegistracijaServis) { }


  ngOnInit() {
    this.role = localStorage.getItem('role');
    this.username = localStorage.getItem('username');
    this.adminServis.getCenovnik().subscribe(data => {
        console.log(data);
        this.cenovnik = data;
        this.cenovnikForma.patchValue(data);
      });
    } 

    onSubmit()
    {
      this.cenovnik = this.cenovnikForma.value;
      console.log(this.cenovnik);
      this.adminServis.postCenovnik(this.cenovnik).subscribe(data => {
        console.log('Uspesno izmenjen cenovnik!');
        
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

