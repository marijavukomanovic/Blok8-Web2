import {LokacijaService} from 'src/app/servisi/lokacija.service';

export class WebSocketLinija{

    lineId: string;
    service: LokacijaService;
    isConnected: boolean;
    notification: string[];
    lan: string;
    lng: string;

    constructor() {
        this.lineId = '0';
        this.service = new LokacijaService();
        this.isConnected = false;
        this.notification = [];
        this.lan = '0';
        this.lng = '0';
      }
}

export class TrenutnaLokacija {
    lineId: string;
    color: string;
    lan: string;
    lng: string;
  
    constructor(){
      this.lineId = '0';
      this.color = 'black';
      this.lan = '0';
      this.lng = '0';
    }
  }