import { Injectable } from '@angular/core';
import { RegistracijaModel } from 'src/app/model/registracijaModel';
import { Observable, Subject, of } from 'rxjs';
import {LoginModel} from 'src/app/model/login-model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import {Router} from '@angular/router';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'  })
};

@Injectable({
  providedIn: 'root'
})
export class RegistracijaServis {

   // ZA LOGIN
  // zavisno od promene username-a, ja ga predstavljam kao Observable na servisu, i onda se subscribujem na taj stream u komponenti
  username: Observable<string>;
  private userSubject: Subject<string>;

  // ZA PRIKAZ INFO OPCIJE (na osnovu role)
  role: Observable<string>;
  private roleSubject: Subject<string>;

  // ZA USER INFO
  editUser: Observable<RegistracijaModel>;
  private editUserSubject: Subject<RegistracijaModel>;


  constructor(private http: HttpClient, private router: Router) {
    this.userSubject = new Subject<string>();
    this.username = this.userSubject.asObservable();

    this.roleSubject = new Subject<string>();
    this.role = this.roleSubject.asObservable();

    this.editUserSubject = new Subject<RegistracijaModel>();
    this.editUser = this.editUserSubject.asObservable();
  }

 private registracijaUrl = "http://localhost:52295/api/Korisnik/Registracija";
  private UserLoginUrl = 'http://localhost:52295/oauth/token';
 
    register(data: RegistracijaModel) : Observable<any>{

      let data1 = new FormData()
     data1.append('file', data.Document)
      //data1.append('data', )//multiform web api

      let httpOptions = {
        headers:{
          "Content-type":"application/json"
        }
      }
      return this.http.post<any>(this.registracijaUrl, data, httpOptions);
    }

    logIn(user: LoginModel) {
      const data = `username=${user.Username}&password=${user.Password}&grant_type=password`;
    // tslint:disable-next-line: no-shadowed-variable
      const httpOptions = {
          headers: {
              'Content-type': 'application/x-www-form-urlencoded'
          }
      };
      const retVal = 'Username or password is incorrect';
      this.http.post<any>(this.UserLoginUrl, data, httpOptions)
    // tslint:disable-next-line: no-shadowed-variable
      .subscribe (data => {
    
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
    
        this.username = decodedJwtData.unique_name;
        this.userSubject.next(decodedJwtData.unique_name);
        this.role = decodedJwtData.role;
        this.roleSubject.next(decodedJwtData.role);
    
      
      });
     
    }
    
    logout() {
      localStorage.clear();
      this.router.navigate(['/']);
    }
    
    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
        return of(result as T);
      };
    }
}