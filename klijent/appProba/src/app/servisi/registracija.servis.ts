import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/servisi/http.servis';
import { RegistracijaModel } from 'src/app/model/registracijaModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegistracijaServis extends HttpService{

  specificUrl = this.url + "/api/Registration/PostRegistration";
  
    register(data: RegistracijaModel) : Observable<any>{
      let httpOptions = {
        headers:{
          "Content-type":"application/json"
        }
      }
      return this.http.post<any>(this.specificUrl, data, httpOptions);
    }

}