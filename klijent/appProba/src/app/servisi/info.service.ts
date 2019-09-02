import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {RegistracijaModel, InfoModel} from 'src/app/model/registracijaModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InfoService{

  private url = "http://localhost:52295";

  constructor(private http: HttpClient) { }

  getInfo(user : string) : Observable<InfoModel>
  {
    return this.http.get<InfoModel>(this.url + "/api/Korisnik/GetInfo/" + user);
  }
  postChangedInfo(user : InfoModel) : Observable<any>{
  
    let data1 = new FormData()

      let httpOptions = {
        headers:{
          "Content-type":"application/json"
        }
      }
      return this.http.post<any>(this.url + "/api/Korisnik/ChangeInfo/" + user,user,httpOptions);
    }

}
