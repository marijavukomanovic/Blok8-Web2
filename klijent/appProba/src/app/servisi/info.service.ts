import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpService} from './http.servis';
import {RegistracijaModel} from 'src/app/model/registracijaModel';

@Injectable({
  providedIn: 'root'
})
export class InfoService extends HttpService{

  getInfo() : Observable<any>
  {
    return this.http.get<any>(this.url + "api/UserInfo/GetInfo/");
  }

  postChangedInfo(user : RegistracijaModel) : Observable<any>{
  
    let data1 = new FormData()

      let httpOptions = {
        headers:{
          "Content-type":"application/json"
        }
      }
      return this.http.post<any>(this.url + "/api/Korisnik/ChangeInfo/" + user,user,httpOptions);//gadja kont,podatak,opcije
    }
}
