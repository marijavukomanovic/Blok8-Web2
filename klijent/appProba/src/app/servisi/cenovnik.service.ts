import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CenovnikService{

  private url = "http://localhost:52295";

  constructor(private http: HttpClient) { }

  getCenovnik(vrstaKarte:number,user : string) : Observable<any>{
    return this.http.get<any>(this.url + "/api/CenaKarte/GetCena/" + vrstaKarte + "/"+user);
  }

  //KupiKartu/{tipKarte}/{username}
  kupiKartu(vrstaKarte : number,username:string) : Observable<any>
  {
    return this.http.get<any>(this.url + "/api/CenaKarte/KupiKartu/" + vrstaKarte + '/' + username);
  }
}
