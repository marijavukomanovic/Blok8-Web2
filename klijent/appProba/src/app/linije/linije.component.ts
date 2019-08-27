import { Component, OnInit,Input, NgZone } from '@angular/core';
import {LinijaService} from 'src/app/servisi/linija.service';
import { MarkerInfo } from 'src/app/model/marker-info.model';
import { GeoLocation } from 'src/app/model/GeoLocation';
import { Polyline } from 'src/app/model/polyline';
import {LineStation} from 'src/app/model/linijaModel'
import {Station} from 'src/app/model/linijaModel'
import {Line} from 'src/app/model/linijaModel'
import {FormBuilder,Validators} from '@angular/forms';
import { from } from 'rxjs';

@Component({
  selector: 'app-linije',
  templateUrl: './linije.component.html',
  styleUrls: ['./linije.component.css'],
  styles: ['agm-map {height: 350px; width: 50%;}'],
})
export class LinijeComponent implements OnInit {
  lines: Array<LineStation>;
  lineIds: Array<string>;
  map = new Map<string, LineStation>();
  iconUrl = { url: 'assets/busicon.png', scaledSize: {width: 30, height: 30}};
  profileForm = this.fb.group({
    lineNumber: ['', [Validators.required, Validators.pattern('[0-9]+(A|B|a|b)')]],
    lineType: ['', Validators.required] ,
    description: [''],
    color: ['blue']
    });
 /* info : string;
  markerInfo:MarkerInfo;
  public polyline:Polyline;
  public zoom:number;
*/
  constructor(private lineService : LinijaService,private ngZone: NgZone,private fb:FormBuilder) { }

  ngOnInit() {
    
     }

    getLines(routeType:number): Array<LineStation> {
      this.lines = new Array<LineStation>();
      this.lineService.getLines(routeType).subscribe(l =>  {
        this.lineIds = l;
      });
      return Array.from( this.map.values() );
    }
  
    

}
