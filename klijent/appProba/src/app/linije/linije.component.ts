import { Component, OnInit, Input, NgZone } from '@angular/core';
import { MarkerInfo } from 'src/app/model/marker-info.model';
import { GeoLocation } from 'src/app/model/geolocation';
import { Polyline } from 'src/app/model/polyline';
import {LinijaService} from 'src/app/servisi/linija.service';
import { Station } from '../model/linijaModel';

@Component({
  selector: 'app-linije',
  templateUrl: './linije.component.html',
  styleUrls: ['./linije.component.css'],
  styles: ['agm-map {height: 500px; width: 1000px;}'] //postavljamo sirinu i visinu mape
})
export class LinijeComponent implements OnInit {

  markerInfo: MarkerInfo;
  public polyline: Polyline;
  public zoom: number;
  public stanica:Station;

  ngOnInit() {
    this.markerInfo = new MarkerInfo(new GeoLocation(45.242268, 19.842954), 
      "assets/ftn.png",
      "Jugodrvo" );

      this.polyline = new Polyline([], 'blue', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});

      this.servisLinija.getLines().subscribe(data =>{
        console.log(data);
        this.stanica = data;
      });
      //this.polyline.addLocation(new GeoLocation(this.stanica.XCoordinate, this.stanica.YCoordinate));
      //this.markerInfo = new MarkerInfo(new GeoLocation(this.stanica.XCoordinate, this.stanica.YCoordinate), 
      //"assets/busicon.png",
      //this.stanica.Address);
  }

  constructor(private ngZone: NgZone,private servisLinija : LinijaService){
  }

  placeMarker($event){
    this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
    console.log($event.coords.lat, $event.coords.lng);
    console.log(this.polyline)
  }

}
