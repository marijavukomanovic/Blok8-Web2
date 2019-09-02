import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { InfoModel } from '../model/registracijaModel';

@Injectable({
  providedIn: 'root'
})
export class KontrolerService {

  private url = "http://localhost:52295";

  constructor(private http: HttpClient) { }

  getUseri(user : string) : Observable<Array<string>>
  {
    return this.http.get<Array<string>>(this.url + "/api/Korisnik/IzlistajKlijente/" + user); 
  }

  getInfo(user : string) : Observable<InfoModel>
  {
    return this.http.get<InfoModel>(this.url + "/api/Korisnik/GetInfo/" + user); 
  }

  saljiStatus(user:string,status:string): Observable<any>
  {
    return this.http.get<any>(this.url + "/api/Korisnik/VerifikacijaKlijenta/" + user +'/'+status);
  }

  getId() : Observable<Array<string>>
  {
    return this.http.get<Array<string>>(this.url + "/api/Korisnik/IzlistajKarte"); 
  }

  getVer(id:string) : Observable<any>
  {
    return this.http.get<any>(this.url + "/api/Korisnik/VerifikujKartu/" + id); 
  }
}
