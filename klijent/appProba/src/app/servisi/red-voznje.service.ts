import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LinijaModel } from 'src/app/model/linijaModel';
import { HttpClient } from '@angular/common/http';
import {redVoznje} from 'src/app/model/redvoznje';

@Injectable({
  providedIn: 'root'
})
export class RedVoznjeService{

  private url = "http://localhost:52295";

  constructor(private http: HttpClient) { }

  getLines(routeType : number) : Observable<any>{
    return this.http.get<any>(this.url + "/api/RedVoznje/GetLinije/" + routeType);
  }
  
//GetRedVoznje/{tipDana}/{linija}
  getSchedule(dayType: number, line: string) : Observable<string>{
    return this.http.get<string>(this.url + "/api/RedVoznje/GetRedVoznje/"  + dayType + "/" + line);
  }

  //GetRedVoznjeNovi/{tipDana}/{linija}/{stringInfo}
  ChangeSchedules(day : number, line : string,time : string) : Observable<any>
  {
    return this.http.get<any>(this.url + "/api/RedVoznje/GetRedVoznjeNovi/"  + day + "/" + line + "/" + time);
  }
  
}

