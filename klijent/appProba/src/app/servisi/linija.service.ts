import { Injectable } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { LinijaModel, LineStation, Station } from 'src/app/model/linijaModel';
import { HttpErrorResponse } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LinijaService{

  private TicketsUrl = 'http://localhost:52295/api/Linije';

  private url = "http://localhost:52295";

  constructor(private http: HttpClient) { }


  getListuLinija(routeType : number): Observable<string[]>{
    return this.http.get<string[]>(this.url + "/api/Linije/GetLineName/" + routeType);
  }

  getLinija(lineName : string): Observable<LineStation>{
      return this.http.get<LineStation>(this.url + "/api/Linije/GetLines/" + lineName);
  }

  obrisiLiniju(lineName : string): Observable<any>{
    return this.http.get<any>(this.url + "/api/Linije/DeleteLine/" + lineName);
}

}


