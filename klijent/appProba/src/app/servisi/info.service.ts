import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpService} from './http.servis';
import {RegistracijaModel} from 'src/app/model/registracijaModel';

@Injectable({
  providedIn: 'root'
})
export class InfoService extends HttpService{

  getInfo(user :string) : Observable<RegistracijaModel>
  {
    return this.http.get<RegistracijaModel>(this.url + "/api/Korisnik/GetInfo/" + user);
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
