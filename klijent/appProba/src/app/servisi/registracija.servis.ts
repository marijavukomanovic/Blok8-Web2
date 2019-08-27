import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/servisi/http.servis';
import { RegistracijaModel } from 'src/app/model/registracijaModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegistracijaServis extends HttpService{

  specificUrl = this.url + "/api/Korisnik/Registracija";
 
    register(data: RegistracijaModel) : Observable<any>{

      let data1 = new FormData()
      data1.append('file', data.Document)
      //data1.append('data', )//multiform web api

      let httpOptions = {
        headers:{
          "Content-type":"application/json"
        }
      }
      return this.http.post<any>(this.specificUrl, data, httpOptions);
    }


}