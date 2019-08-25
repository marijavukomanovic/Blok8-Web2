import { Component, OnInit } from '@angular/core';
import {AdminService} from 'src/app/servisi/admin.service';

@Component({
  selector: 'app-dodavanje-cenovnika',
  templateUrl: './dodavanje-cenovnika.component.html',
  styleUrls: ['./dodavanje-cenovnika.component.css']
})
export class DodavanjeCenovnikaComponent implements OnInit {
info:string;
  constructor(private adminServis : AdminService) { }


  ngOnInit() {
    this.adminServis.ShowDatum().subscribe(data => {
        console.log(data);
        this.info = data;
      });
    

  /*ShowCenovnik(OD:string,DO:string,cena1:number,cena2:number,cena3:number,cena4:number){
    this.adminServis.ShowCenovnik(OD,DO).subscribe(data => {
      console.log(data);
      this.info = data;
    });*/
  }
}

