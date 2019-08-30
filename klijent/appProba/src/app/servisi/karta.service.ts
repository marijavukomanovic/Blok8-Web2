import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from 'src/app/servisi/http.servis';
import { KartaModel } from '../model/karteModel';

@Injectable({
  providedIn: 'root'
})
export class KartaService extends HttpService{

  //IzlistajMojeKarte/{username}
  getKarte(user : string) : Observable<Array<KartaModel>>{
    return this.http.get<Array<KartaModel>>(this.url + "/api/CenaKarte/IzlistajMojeKarte/" + user);
  }

}
