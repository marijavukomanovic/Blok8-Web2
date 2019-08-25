import { Injectable } from '@angular/core';
import {HttpService} from './http.servis';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService extends HttpService{

  ShowDatum() : Observable<any>{
    return this.http.get<any>(this.url + "/api/CenaKarte/GetPoslednjiDatum");
  }

  ShowCenovnik(od : string,dO:string) : Observable<any>{
    return this.http.get<any>(this.url + "/api/CenaKarte/Cenovnik/" + od);
  }
}
