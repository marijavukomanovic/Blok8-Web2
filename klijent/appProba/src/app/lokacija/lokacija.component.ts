import { Component, OnInit,NgZone, NgModule } from '@angular/core';
import { Polyline } from 'src/app/model/polyline';
import { Station, ListaStanica, LineStation } from 'src/app/model/linijaModel';
import { LinijaService } from 'src/app/servisi/linija.service';
import { GeoLocation } from 'src/app/model/GeoLocation';
import {LokacijaService} from 'src/app/servisi/lokacija.service';
import { GoogleMapsAPIWrapper, MapsAPILoader } from '@agm/core';
import { MarkerInfo } from 'src/app/model/marker-info.model';
import { AgmCoreModule } from '@agm/core';            // @agm/core
import { AgmDirectionModule } from 'agm-direction';
import { groupBy } from 'rxjs/operators';
import { provideRoutes } from '@angular/router';

@Component({
  selector: 'app-lokacija',
  templateUrl: './lokacija.component.html',
  styleUrls: ['./lokacija.component.css'],
  styles: ['agm-map {height: 500px; width: 1220px;}'],
  
})



export class LokacijaComponent implements OnInit {

  public polyline: Polyline;
  public polylineRT: Polyline;  
  public zoom: number = 15;
  startLat : number = 45.242268;
  startLon : number = 19.842954;

  listaStanica : ListaStanica;
  stanica : Station;
  linijaStanica : LineStation;

  options : string[];
  options1: any;
  stations : Array<Station> = [];
  buses : any[];
  busImgIcon : any = {url:"assets/busicon.png", scaledSize: {width: 50, height: 50}};
  autobusImgIcon : any = {url:"assets/busImage.png", scaledSize: {width: 50, height: 50}};

  isConnected: boolean;
  notifications: string[];
  time: number[] = [];

  latitude : number ;
  longitude : number;
  marker: MarkerInfo = new MarkerInfo(new GeoLocation(this.startLat,this.startLon),"","");

  isChanged : boolean = false;

  constructor(private mapsApiLoader : MapsAPILoader,private notifForBL : LokacijaService, private ngZone: NgZone, private lineService : LinijaService) {
    this.isConnected = false;
    this.notifications = [];
    // this.checkConnection();
    // //this.startTimer();
    // this.subscribeForTime();
   }

  ngOnInit() {
    this.isChanged = false;
   this.listaStanica = new ListaStanica();
   this.stanica = new Station("","",0,0);
   this.linijaStanica = new LineStation("","","","");
        this.options = ["12A","13A","7A","18","2c"];

    //inicijalizacija polyline
    this.polyline = new Polyline([], 'blue', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});
  
    //za hub
    this.subscribeForTime();
   this.checkConnection();
      
      this.stations = [];
    // this.clickService.click(this.stations).subscribe(data =>
    //   {
        
    //     console.log("data bus location ", data);
        
        
    //   });
  }

  onSelectionChangeNumber(event){
    this.isChanged = true;
    this.stations = [];
    this.polyline.path = [];
    if(event.target.value == "")
    {
      this.isChanged = false;
      this.stations = [];
      this.polyline.path = [];
      this.longitude = 0;
      this.latitude =0;
      this.stopTimer();
    }else
    {
      this.longitude = 0;
      this.latitude =0;
      this.stopTimer();
      this.getStationsByLineNumber(event.target.value);   
    
     // this.notifForBL.StartTimer(); 
    }
    
  }

  getStationsByLineNumber(lineNumber : string){
    this.lineService.getStaniceZaLiniju(lineNumber).subscribe( element => {

      if(element.LineId == lineNumber)
      {
        this.stations = element.Stations;
        //console.log(this.stations);
        for(var i=0; i<this.stations.length; ++i){
          this.polyline.addLocation(new GeoLocation(this.stations[i].XCoordinate, this.stations[i].YCoordinate));
        }
        
        this.lineService.click(element.LineId).subscribe(data =>
          {
            
            console.log("data bus location ", data);
            this.startTimer();
            //this.subscribeForTime();
          });
      }
    });
    
  }


  private checkConnection(){
    this.notifForBL.startConnection().subscribe(e => {
      this.isConnected = e; 
         if (e) {
          // this.notifForBL.StartTimer()
         }
    });
  }  

 public subscribeForTime() {
    this.notifForBL.registerForTimerEvents().subscribe(e => this.onTimeEvent(e));
  }


  public onTimeEvent(pos: number[]){
    this.ngZone.run(() => {
      console.log("STIGLOOOO");
      this.latitude = pos[0];
      this.longitude=pos[1];
      this.time = pos; 
       if(this.isChanged){
         this.latitude = pos[0];
          this.longitude = pos[1];
          console.log("pos: ", this.latitude, this.longitude);
          //this.isChanged = false;
       }else{
          this.latitude = 0;
          this.longitude = 0;
       }
    });      
  }  

  public startTimer() {    
    this.notifForBL.StartTimer();
  }

  public stopTimer() {
    this.notifForBL.StopTimer();
    console.log("valjda stopira timer")
    this.time = null;
  }

}
