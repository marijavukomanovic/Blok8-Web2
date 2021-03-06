import { Component, OnInit, Input } from '@angular/core';
import { RedVoznjeService } from 'src/app/servisi/red-voznje.service';
import { RouteType } from '../model/enums';
import { LinijaService } from 'src/app/servisi/linija.service';
import { strictEqual } from 'assert';
import { NgModel, Validators } from '@angular/forms';
import {FormBuilder,FormGroup} from '@angular/forms';
import { redVoznje } from '../model/redvoznje';
import { Router } from '@angular/router';
import { RegistracijaServis } from '../servisi/registracija.servis';

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
  Redvoznje : redVoznje;
  separators:Array<string>;
  redV :redVoznje ={
red: '',
  };
  @Input() redVoznje = this.fb.group({
    redvoznje: ['', Validators.required],

    });



  //redVoznje : redVoznje;

  constructor(private redVoznjService : RedVoznjeService,private fb: FormBuilder,private router:Router,private userService:RegistracijaServis) { }

  ngOnInit() {
    this.username = localStorage.getItem('username');
    this.role = localStorage.getItem('role');
    
    this.prikazi=false;
    this.separators=["-",":",".","#","@","/"];
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
     // this.time=data;
     this.redV.red = data;
     this.redVoznje.patchValue({
       redvoznje :this.redV.red
     }
       );
     //this.str = this.redV.red;
     //this.redVoznje.patchValue(data);
     //this.redVoznje.patchValue({
      // redvoznje:data.red,
     //})
      this.prikazi=true;
    });
    }

    ChangeSchedules(day : number, lines : string)
    {
     //console.log(times);
     //this.redV = this.redVoznje.value;
     this.redV.red = this.redVoznje.controls.redvoznje.value;
     //console.log(this.redV);
     this.Redvoznje = this.redV;
for (let index = 0; index < this.separators.length; index++) {
  this.Redvoznje.red=this.Redvoznje.red.split(this.separators[index]).join(" ");
  
}

     //this.Redvoznje.red = this.redV.red.split("\n").join('');
     console.log(this.Redvoznje.red);
      this.redVoznjService.ChangeSchedules(day,lines,this.Redvoznje.red).subscribe(data1=>{
        console.log(data1);
        //this.time=data1;
        this.prikazi=true;
      });
    }

    DeleteSchedules(dayType : number, lineName: string)
    {
      //lineName.replace()
      //console.log(lineName);
      this.redVoznjService.IzbrisiRedVoznje(dayType,lineName).subscribe(data1=>{
        console.log(data1);
        //this.time=data1;
        
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
