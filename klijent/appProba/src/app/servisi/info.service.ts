import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {RegistracijaModel} from 'src/app/model/registracijaModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InfoService{

  private url = "http://localhost:52295";

  constructor(private http: HttpClient) { }

  getInfo(user : string) : Observable<RegistracijaModel>
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
      return this.http.post<any>(this.url + "/api/Korisnik/ChangeInfo/" + user,user,httpOptions);
    }
 /* postChangedInfo(user:RegistracijaModel):Observable<RegistracijaModel>
  {
   return this.http.post<RegistracijaModel>(this.url+"/api/Korisnik/ChangeInfo/"+user); 
  }
  /*postChangedInfo(user : RegistracijaModel) : Observable<any>{
  
    let data1 = new FormData()

      let httpOptions = {
        headers:{
          "Content-type":"application/json"
        }
      }
      return this.http.post<any>(this.url + "/api/Korisnik/ChangeInfo/" + user,user,httpOptions);//gadja kont,podatak,opcije
    }*/
}
