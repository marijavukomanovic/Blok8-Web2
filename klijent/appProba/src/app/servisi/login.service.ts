import { Injectable } from '@angular/core';
import { HttpService } from './http.servis';
import { LoginModel } from '../model/login-model';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService extends HttpService{

  specificUrl = this.url + "/api/LogIn/LogInUsern";

  login(data: LoginModel) : Observable<any>{
    let httpOptions = {
      headers:{
        "Content-type":"application/json"
      }
    }
    //let params = `username=${data.UserName}&password=${data.Password}&grant_type=password`;
    return this.http.post(this.specificUrl, data, httpOptions);
  }
}
