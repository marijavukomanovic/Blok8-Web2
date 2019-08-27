import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-izmena-cenovnika',
  templateUrl: './izmena-cenovnika.component.html',
  styleUrls: ['./izmena-cenovnika.component.css']
})
export class IzmenaCenovnikaComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
 /*import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import {AdminService} from 'src/app/servisi/admin.service';
import { CenovnikModel } from '../model/cenovnikModel';

@Component({
  selector: 'app-dodavanje-cenovnika',
  templateUrl: './dodavanje-cenovnika.component.html',
  styleUrls: ['./dodavanje-cenovnika.component.css']
})
export class DodavanjeCenovnikaComponent implements OnInit {
info:string;
cenovnik : CenovnikModel = {
  OD : this.info,
  DO : '',
  cenaVremenska : 0,
  cenaDnevna : 0,
  cenaMesecna : 0,
  cenaGodisnja : 0,
};

cenovnikForma = this.fb.group(
  {
    OD : [this.cenovnik.OD],
    DO:[''],
    cenaVremenska :[''],
    cenaDnevna:[''],
    cenaMesecna:[''],
    cenaGodisnja:['']
  }
);

  constructor(private adminServis : AdminService,private fb: FormBuilder) { }


  ngOnInit() {
    this.adminServis.ShowDatum().subscribe(data => {
        console.log(data);
        this.info = data;
        
      });
    } 

    onSubmit()
    {
      this.cenovnik = this.cenovnikForma.value;
      this.cenovnik.OD = this.info;
      console.log(this.cenovnik);
      console.log(this.cenovnikForma.value);
      this.adminServis.ShowCenovnik(this.cenovnik).subscribe(data => {
        console.log('Registration successfully done.');
        
      });
    }

}*/