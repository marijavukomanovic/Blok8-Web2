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
  
    let data1 = new FormData()

      let httpOptions = {
        headers:{
          "Content-type":"application/json"
        }
      }
      return this.http.post<any>(this.url + "/api/CenaKarte/Cenovnik/" + cenovnik,cenovnik,httpOptions);
    }

    getCenovnik()
    {
      
    }
  }
