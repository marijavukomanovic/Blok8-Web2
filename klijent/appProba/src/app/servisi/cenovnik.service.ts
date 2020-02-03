import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient} from '@angular/common/http';
import {PayPalModel} from '../model/payPalModel';

@Injectable({
  providedIn: 'root'
})
export class CenovnikService{

  private url = "http://localhost:52295";

  constructor(private http: HttpClient) { }

  getCenovnik(vrstaKarte:number,username : string) : Observable<number>{

    //console.log("Usao");
    return this.http.get<number>(this.url + "/api/CenaKarte/GetCena/" + vrstaKarte + '/' + username);
  }

  //KupiKartu/{tipKarte}/{username}
  kupiKartu(vrstaKarte : number,username:string) : Observable<any>
  {
    return this.http.get<any>(this.url + "/api/CenaKarte/KupiKartu/" + vrstaKarte + '/' + username);
  }

  addPayPal(payPal : PayPalModel): Observable<any> {
    let data1 = new FormData()

      let httpOptions = {
        headers:{
          "Content-type":"application/json"
        }
      }
    return this.http.post<PayPalModel>(this.url + "/api/PayPals/PostPaypal/" + payPal,payPal,httpOptions);
  }


  kupiKartuPayPal(vrstaKarte : number,username:string,id : number) : Observable<any>
  {
    return this.http.get<any>(this.url + "/api/CenaKarte/KupiKartuPayPal/" + vrstaKarte + '/' + username + '/' + id);
  }

}
