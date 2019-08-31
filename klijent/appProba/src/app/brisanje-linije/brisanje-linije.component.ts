import { Component, OnInit, Input, NgZone } from '@angular/core';
import { MarkerInfo } from 'src/app/model/marker-info.model';
import { GeoLocation } from 'src/app/model/geolocation';
import { Polyline } from 'src/app/model/polyline';
import {LinijaService} from 'src/app/servisi/linija.service';
import { Station, LineStation } from '../model/linijaModel';
import { Marker } from '@agm/core/services/google-maps-types';
import {Router} from '@angular/router';
import {AuthService} from 'src/app/auth/auth.service';

@Component({
  selector: 'app-brisanje-linije',
  templateUrl: './brisanje-linije.component.html',
  styleUrls: ['./brisanje-linije.component.css'],
  styles: ['agm-map {height: 500px; width: 850px;}'] //postavljamo sirinu i visinu mape
})
export class BrisanjeLinijeComponent implements OnInit {
  markerInfo: MarkerInfo;
  public polyline: Polyline;
  public zoom: number;
  public stanica1: Array<Station>;
  public sta : Station;
  lines : string[];
  lineStation : LineStation;
  pritisnuto : boolean;
  username : string;
role:string;
obrisi : boolean;

  ngOnInit() {
    this.obrisi = false;
    this.role = localStorage.getItem('role');
    this.username = localStorage.getItem('username');
    this.pritisnuto = false;
    this.markerInfo = new MarkerInfo(new GeoLocation(45.242268, 19.842954), 
      "",
      "Jugodrvo" );

      this.polyline = new Polyline([], 'blue', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});
  }

  constructor(private ngZone: NgZone,private servisLinija : LinijaService,private router: Router,private userService:AuthService){ }
  
  getListuLinija(routeType : number)
  {
    this.obrisi = false;
    this.servisLinija.getListuLinija(routeType).subscribe(data => {
      console.log(data);
      this.lines = data;
    });
  }

  getLinija(lineName : string)
  {
    this.obrisi = false;
    this.servisLinija.getLinija(lineName).subscribe(data => {
      console.log(data);
      this.lineStation = data;
      this.pritisnuto = true;
      this.polyline = new Polyline([], this.lineStation.Color, { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});
      for (let index = 0; index < this.lineStation.Stations.length; index++) {
        const element = this.lineStation.Stations[index];
        this.polyline.addLocation(new GeoLocation(element.XCoordinate, element.YCoordinate));
      }
    });
    this.obrisi = true;
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

  obrisiLiniju(lineName : string )
  {
    this.servisLinija.obrisiLiniju(lineName).subscribe(data => {
      console.log(data);
    });
    this.router.navigate(['brisanjeLinije']);
  }

}
