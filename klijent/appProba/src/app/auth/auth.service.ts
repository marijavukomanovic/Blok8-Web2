import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, pipe, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { User } from './user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  isLoggedIn = false;

  loginUrl: string = 'http://localhost:52295/oauth/token';

  constructor(private http: HttpClient) { }

  login(user: User, callback: any) {
    const data = `username=${user.username}&password=${user.password}&grant_type=password`;
    alert(data);
    // tslint:disable-next-line: no-shadowed-variable
      const httpOptions = {
          headers: {
              'Content-type': 'application/x-www-form-urlencoded'
          }
      };
      this.http.post<any>(this.loginUrl, data, httpOptions).subscribe (data => {

        const jwt = data.access_token;
    
        const jwtData = jwt.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
    
        const role = decodedJwtData.role;
    
        if (localStorage.length === 0) {
          localStorage.setItem('jwt', jwt);
          localStorage.setItem('role', role);
          localStorage.setItem('username',  decodedJwtData.unique_name);
        }
        callback();
      }, erro=>
      alert('blaaa'));
    
  }

  logout(): void {
    this.isLoggedIn = false;
    localStorage.removeItem('jwt');
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}