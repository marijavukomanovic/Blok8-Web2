import { Injectable } from '@angular/core';
import {HttpService} from './http.servis';
import {Observable} from 'rxjs';
import {CenovnikModel} from 'src/app/model/cenovnikModel';

@Injectable({
  providedIn: 'root'
})
export class AdminService extends HttpService{

  ShowDatum() : Observable<any>{
    return this.http.get<any>(this.url + "/api/CenaKarte/GetPoslednjiDatum");
  }

  ShowCenovnik(cenovnik : CenovnikModel) : Observable<any>{
    return this.http.get<any>(this.url + "/api/CenaKarte/Cenovnik/" + cenovnik );
  }
}
