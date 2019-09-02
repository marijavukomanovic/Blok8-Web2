import { Component, OnInit, NgZone } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MarkerInfo } from 'src/app/model/marker-info.model';
import { GeoLocation } from 'src/app/model/geolocation';
import { Polyline } from 'src/app/model/polyline';
import {LinijaService} from 'src/app/servisi/linija.service';
import { Station, LineStation, StanicaModel ,StanicaModelirana} from '../model/linijaModel';
import { Marker } from '@agm/core/services/google-maps-types';
import {Router} from '@angular/router';
import {RegistracijaServis} from 'src/app/servisi/registracija.servis';

@Component({
  selector: 'app-dodavanje-stanice',
  templateUrl: './dodavanje-stanice.component.html',
  styleUrls: ['./dodavanje-stanice.component.css'],
  styles: ['agm-map {height: 500px; width: 620px;}'] //postavljamo sirinu i visinu mape
})
export class DodavanjeStaniceComponent implements OnInit {
  
  markerInfo: MarkerInfo;
  public polyline: Polyline;

  message:string;
 username:string;
 role:string;
 lines : Array<string>;
 pocetak :boolean;
 x : number;
 y:number;
 stat : StanicaModel = {
  Name : '',
  Address: '',
  Line: '',
  X : 0,
  Y : 0
 };

stanica : StanicaModelirana ={
  Name : '',
  Address: '',
  Line: '',
};

stanicaForma = this.fb.group(
  {
    Name : [''],
    Address :[''],
    Line:[''],
  }
);



  constructor(private ngZone: NgZone,private fb: FormBuilder,private linijaServis:LinijaService,private userService:RegistracijaServis,private router:Router) { }

  ngOnInit() {
    this.role = localStorage.getItem('role');
    this.username = localStorage.getItem('username');
    this.pocetak = true;
    this.x = 0;
    this.y=0;

      this.polyline = new Polyline([], 'blue', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});

    this.linijaServis.sveLinije().subscribe(data => {
      this.lines = data;
      //console.log('!');
    });   
  }

  placeMarker($event){
    if (this.pocetak)
    {
      this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
      //console.log($event.coords.lat, $event.coords.lng);
      this.pocetak=false;
      this.x = $event.coords.lat;
      this.y=$event.coords.lng;
    }
    else
    {
      this.polyline.removeLocation();
      this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
      //console.log($event.coords.lat, $event.coords.lng);
      this.x = $event.coords.lat;
      this.y=$event.coords.lng;
    }
    }
    
    onSubmit()
    {
      if( this.stanicaForma.controls['Name'].value=="" || this.stanicaForma.controls['Address'].value=="" || this.stanicaForma.controls['Line'].value=="")
      {
        this.message = "Sva polja moraju biti popunjena!";
      }
      else if (this.x==0 || this.y==0)
      {
        this.message = "Izaberite lokaciju stanice na mapi!"
      }
      else
      {
        this.message = "";
          this.stanica = this.stanicaForma.value;
          this.stat.Address = this.stanica.Address;
          this.stat.Line = this.stanica.Line;
          this.stat.Name = this.stanica.Name;
          this.stat.X = this.x;
          this.stat.Y = this.y;
          console.log(this.stat);
          this.linijaServis.dodajStanicu(this.stat).subscribe(data => {
            console.log(data);
          });
          
          this.router.navigate(['/dodavanjeStanice']);
          
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

