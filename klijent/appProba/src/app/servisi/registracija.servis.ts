import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/servisi/http.servis';
import { RegistracijaModel } from 'src/app/model/registracijaModel';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegistracijaServis extends HttpService{

  specificUrl = this.url + "/api/Korisnik/Registracija";
 
    /*register(data: RegistracijaModel) : Observable<any>{

      let data1 = new FormData()
      data1.append('file', data.Document)
      //data1.append('data', )//multiform web api

      let httpOptions = {
        headers:{
          "Content-type":"application/json"
        }
      }
      return this.http.post<any>(this.specificUrl, data, httpOptions);
    }*/

    register(user: RegistracijaModel) : Observable<any>{

      return Observable.create((observer) => {
          let data = user;
          let httpOptions={
              headers:{
                  "Content-type": "application/json"
              }
          }
          this.http.post<any>(this.specificUrl,data,httpOptions).subscribe(data => {
              observer.next("uspesno");
              observer.complete();
          },
          err => {
              console.log(err);
              observer.next("neuspesno");
              observer.complete();
          });
      });
   
  }

  uploadImage(data: any, id: string) : Observable<any> {
      return Observable.create((observer) => {
          let httpOptions = {
              headers: new HttpHeaders().delete('Content-Type')
          }
          this.http.post<any>(this.url + "/api/Korisnik/UplaodPicture/" + id,data,httpOptions).subscribe(data => {
              observer.next("uspesno");
              observer.complete();
          },
          err => {
              console.log(err);
              observer.next("neuspesno");
              observer.complete();
          });
      });
  }

}