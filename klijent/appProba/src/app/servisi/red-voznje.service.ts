import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from 'src/app/servisi/http.servis';
import { LinijaModel } from 'src/app/model/linijaModel';

@Injectable({
  providedIn: 'root'
})
export class RedVoznjeService extends HttpService{

  getLines(routeType : number) : Observable<any>{
    return this.http.get<any>(this.url + "/api/RedVoznje/GetLinije/" + routeType);
  }
  
//GetRedVoznje/{tipDana}/{linija}
  getSchedule(dayType: number, line: string) : Observable<any>{
    return this.http.get<any>(this.url + "/api/RedVoznje/GetRedVoznje/"  + dayType + "/" + line);
  }

  //GetRedVoznjeNovi/{tipDana}/{linija}/{stringInfo}
  ChangeSchedules(day : number, line : string,time : string) : Observable<any>
  {
    return this.http.get<any>(this.url + "/api/RedVoznje/GetRedVoznjeNovi/"  + day + "/" + line + "/" + time);
  }
  
}

