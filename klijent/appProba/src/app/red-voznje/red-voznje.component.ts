import { Component, OnInit } from '@angular/core';
import { RedVoznjeService } from 'src/app/servisi/red-voznje.service';
import { RouteType } from '../model/enums';
import { LinijaService } from 'src/app/servisi/linija.service';

@Component({
  selector: 'app-red-voznje',
  templateUrl: './red-voznje.component.html',
  styleUrls: ['./red-voznje.component.css']
})
export class RedVoznjeComponent implements OnInit {

  lines : string[];
  time : string;
  username:string;
  role:string;
  prikazi:boolean;

  constructor(private redVoznjService : RedVoznjeService) { }

  ngOnInit() {
    this.username = localStorage.getItem('username');
    this.role = localStorage.getItem('role');
    this.prikazi=false;
  }

  ShowLines(routeType : number){
    this.redVoznjService.getLines(routeType).subscribe(data => {
      console.log(data);
      this.lines = data;
    });
  }
  ShowSchedules(dayType: number, line:string){
    this.redVoznjService.getSchedule( dayType, line).subscribe(data=>{
      console.log(data);
      this.time=data;
      this.prikazi=true;
    });

    }
  }
