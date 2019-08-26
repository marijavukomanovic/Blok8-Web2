import { Component, OnInit } from '@angular/core';
import { RedVoznjeService } from 'src/app/servisi/red-voznje.service';
import { RouteType } from '../model/enums';
import { LinijaService } from 'src/app/servisi/linija.service';
import { strictEqual } from 'assert';
import { NgModel } from '@angular/forms';
import {FormBuilder,FormGroup} from '@angular/forms';
import { redVoznje } from '../model/redvoznje';

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
  str : string;
  //redVoznje : redVoznje;

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
      //this.redVoznje.red = data.toString;
      this.time=data;
      this.prikazi=true;
    });
    }

    ChangeSchedules(day : number, lines : string,time:string)
    {
     //console.log(times);
      this.redVoznjService.ChangeSchedules(day,lines,time).subscribe(data1=>{
        console.log(data1);
        this.time=data1;
        this.prikazi=true;
      });
    }

    ChangeSchedules1(day : number, lines : string,times:string)
    {
      
      this.redVoznjService.ChangeSchedules(day,lines,times).subscribe(data1=>{
        console.log(data1);
        //this.time=data;
        this.prikazi=true;
      });
    }
    
  }
