import { Injectable } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { HttpService } from 'src/app/servisi/http.servis';
import { LinijaModel, LineStation, Station } from 'src/app/model/linijaModel';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LinijaService extends HttpService{

  private TicketsUrl = 'http://localhost:52295/api/Linije';
  /*getLines(routeType : number) : Observable<any>{
    return this.http.get<any>(this.url + "/api/Linije/GetLines/" + routeType);
  }*/
 
  /*getLines() : Observable<Station>{
    return this.http.get<Station>(this.url + "/api/Linije/GetLines");
  }*/

  getLines(): Observable<Station> {
    const url = `${this.TicketsUrl}/Getlines`;
    return this.http.get<Station>(url).pipe(
      
    );
  }

  errorHandler(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred: ', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}` + `body was: ${error.error.error_description}`
      );
    }

    return throwError('Something bad happend, please try again later...');
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
  }


