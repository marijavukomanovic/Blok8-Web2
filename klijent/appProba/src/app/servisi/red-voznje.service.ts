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

  getSchedule(dayType: number, line: string) : Observable<any>{
    return this.http.get<any>(this.url + "/api/RedVoznje/GetRedVoznje/"  + dayType + "/" + line);
  }
  //GetRedVoznje/{tipDana}/{linija}
}

