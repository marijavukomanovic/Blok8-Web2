import { Component, OnInit, Input, NgZone } from '@angular/core';
import { MarkerInfo } from 'src/app/model/marker-info.model';
import { GeoLocation } from 'src/app/model/geolocation';
import { Polyline } from 'src/app/model/polyline';
import {LinijaService} from 'src/app/servisi/linija.service';
import { Station, LineStation } from '../model/linijaModel';
import { Marker } from '@agm/core/services/google-maps-types';

@Component({
  selector: 'app-linije',
  templateUrl: './linije.component.html',
  styleUrls: ['./linije.component.css'],
  styles: ['agm-map {height: 500px; width: 620px;}'] //postavljamo sirinu i visinu mape
})
export class LinijeComponent implements OnInit {

  markerInfo: MarkerInfo;
  public polyline: Polyline;
  public zoom: number;
  public stanica1: Array<Station>;
  public sta : Station;
  lines : string[];
  lineStation : LineStation;
  pritisnuto : boolean;

  ngOnInit() {
    this.pritisnuto = false;
    this.markerInfo = new MarkerInfo(new GeoLocation(45.242268, 19.842954), 
      "",
      "Jugodrvo" );

      this.polyline = new Polyline([], 'blue', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});

      
      /*this.servisLinija.getLines().subscribe(data =>{
        console.log(data);

        for (let index = 0; index < data.length; index++) {
          this.sta = data[index];
        this.polyline.addLocation(new GeoLocation(this.sta.XCoordinate, this.sta.YCoordinate));
        
        
            
            this.markerInfo =  new MarkerInfo(new GeoLocation(this.sta.XCoordinate, this.sta.YCoordinate), 
            "assets/ftn.png",
            this.sta.Address);
       
        }
        
      });*/
      
  }

  constructor(private ngZone: NgZone,private servisLinija : LinijaService){
  }

  placeMarker($event){
    this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
    console.log($event.coords.lat, $event.coords.lng);
    console.log(this.polyline)
  }

  getListuLinija(routeType : number)
  {
    this.servisLinija.getListuLinija(routeType).subscribe(data => {
      console.log(data);
      this.lines = data;
    });
  }

  getLines(lineName : string)
  {
    this.servisLinija.getLines(lineName).subscribe(data => {
      console.log(data);
      this.lineStation = data;
      this.pritisnuto = true;
      this.polyline = new Polyline([], this.lineStation.Color, { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});
      for (let index = 0; index < this.lineStation.Stations.length; index++) {
        const element = this.lineStation.Stations[index];
        this.polyline.addLocation(new GeoLocation(element.XCoordinate, element.YCoordinate));
      }
    });
  }

}
