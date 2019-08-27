import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import {AdminService} from 'src/app/servisi/admin.service';
import { CenovnikModel } from '../model/cenovnikModel';

@Component({
  selector: 'app-izmena-cenovnika',
  templateUrl: './izmena-cenovnika.component.html',
  styleUrls: ['./izmena-cenovnika.component.css']
})
export class IzmenaCenovnikaComponent implements OnInit {

  info:string;

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

  constructor(private adminServis : AdminService,private fb: FormBuilder) { }


  ngOnInit() {
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

}

