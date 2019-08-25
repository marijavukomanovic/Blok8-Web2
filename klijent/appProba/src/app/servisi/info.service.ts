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
    return this.http.get<RegistracijaModel>(this.url + "api/Korisnik/GetInfo/" + user);
  }
}
