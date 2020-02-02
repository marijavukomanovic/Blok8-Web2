import { Injectable, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';

 declare var $ : any;

@Injectable()
export class LokacijaService {

 

  private proxy: any;  
  private proxyName: string = 'notificationsBus';  
  private connection: any;  
  public connectionExists: boolean; 

  public notificationReceived: EventEmitter<string>;  
  
  constructor() {
    this.notificationReceived = new EventEmitter<string>();
    this.connectionExists = false;  
    // create a hub connection  
    this.connection = $.hubConnection("http://localhost:52295/");
    this.connection.qs = {"token" : "Bearer "+ localStorage.jwt};
    // create new proxy with the given name 
    this.proxy = this.connection.createHubProxy(this.proxyName); 
    //this.registerForTimerEvents();
    //this.startConnection();
    
    
   }

  public startConnection(): Observable<boolean> { 
    
    return Observable.create((observer) => {
      
        this.connection.start()
        .done((data: any) => {  
            console.log('Now connected ' + data.transport.name + ', connection ID= ' + data.id)
            this.connectionExists = true;

            observer.next(true);
            observer.complete();
            //this.StartTimer();
        })
        .fail((error: any) => {  
            console.log('Could not connect ' + error);
            this.connectionExists = false;

            observer.next(false);
            observer.complete(); 
        });  
      });
  } 

  public registerForTimerEvents() : Observable<number[]> {
      
    return Observable.create((observer) => {

        this.proxy.on('setRealTime', (data: number[]) => {
          
          console.log("data iz servisa",data);         
            observer.next(data);
            //console.log("data iz servisa",data);
            //this.notificationReceived.emit(data);
        });  
    });      
  }

  public StopTimer() {
      this.proxy.invoke("StopTimeServerUpdates");
  }

  public StartTimer() {
      this.proxy.invoke("TimeServerUpdates");
  }
}
