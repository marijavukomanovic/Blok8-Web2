import { Component, OnInit } from '@angular/core';
import {CenovnikService} from 'src/app/servisi/cenovnik.service';
import { flatMap } from 'rxjs/operators';

@Component({
  selector: 'app-cenovnik',
  templateUrl: './cenovnik.component.html',
  styleUrls: ['./cenovnik.component.css']
})
export class CenovnikComponent implements OnInit {
  info : number;
  username:string;
  role:string;
  pritisnuto :boolean;

  constructor(private cenovnikService : CenovnikService) { }

  ngOnInit() {
this.username=localStorage.getItem('username');
this.role=localStorage.getItem('role');
this.pritisnuto=false;
//this.cenovnikMo = new CenovnikMo(0,this.username);
  }

  ShowCena(vrstaKarte : number){
    this.pritisnuto=true;
    this.cenovnikService.getCenovnik(vrstaKarte,this.username).subscribe(data => {
      console.log(data);
      this.info = data; 
    });
  }

  kupiKartu(vrstaKarte : number)
  {
    this.cenovnikService.kupiKartu(vrstaKarte,this.username).subscribe(data => {
      console.log(data);
    });
  }

}
