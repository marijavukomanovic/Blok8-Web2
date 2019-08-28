import { Injectable } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { HttpService } from 'src/app/servisi/http.servis';
import { LinijaModel, LineStation, Station } from 'src/app/model/linijaModel';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LinijaService extends HttpService{

  private TicketsUrl = 'http://localhost:52295/api/Linije';
 
  /*getLines() : Observable<Station>{
    return this.http.get<Station>(this.url + "/api/Linije/GetLines");
  }*/


  getListuLinija(routeType : number): Observable<string[]>{
    return this.http.get<string[]>(this.url + "/api/Linije/GetLineName/" + routeType);
  }

  getLines(lineName : string): Observable<LineStation>{
      return this.http.get<LineStation>(this.url + "/api/Linije/GetLines/" + lineName);
  }
}


