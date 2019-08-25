import { Component, OnInit } from '@angular/core';
import {CenovnikService} from 'src/app/servisi/cenovnik.service';

@Component({
  selector: 'app-cenovnik',
  templateUrl: './cenovnik.component.html',
  styleUrls: ['./cenovnik.component.css']
})
export class CenovnikComponent implements OnInit {
  info : number;
  username:string;
  role:string;

  constructor(private cenovnikService : CenovnikService) { }

  ngOnInit() {
this.username=localStorage.getItem('username');
this.role=localStorage.getItem('role');
  }

  ShowCena(vrstaKarte : number){
    this.cenovnikService.getCenovnik(vrstaKarte).subscribe(data => {
      console.log(data);
      this.info = data;

    });
  }

}
