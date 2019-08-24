import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from 'src/app/servisi/http.servis';

@Injectable({
  providedIn: 'root'
})
export class CenovnikService extends HttpService{

  getCenovnik(vrstaKarte : number) : Observable<any>{
    return this.http.get<any>(this.url + "/api/CenaKarte/GetCena/" + vrstaKarte);
  }
}
