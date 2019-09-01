import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KartaModel } from '../model/karteModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class KartaService{

  private url = "http://localhost:52295";

  constructor(private http: HttpClient) { }

  //IzlistajMojeKarte/{username}
  getKarte(user : string) : Observable<Array<KartaModel>>{
    return this.http.get<Array<KartaModel>>(this.url + "/api/CenaKarte/IzlistajMojeKarte/" + user);
  }

}
