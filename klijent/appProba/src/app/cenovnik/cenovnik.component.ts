import { Component, OnInit ,AfterViewChecked} from '@angular/core';
import {CenovnikService} from 'src/app/servisi/cenovnik.service';
import { flatMap } from 'rxjs/operators';
//import { NgxPayPalModule } from 'ngx-paypal';
//import { PayPalConfig } from 'ngx-paypal'
//import { IPayPalConfig,ICreateOrderRequest } from 'ngx-paypal';
import {IPayPalConfig,ICreateOrderRequest } from 'ngx-paypal';
import { PayPalModel } from 'src/app/model/paypalModel';
import { Router } from '@angular/router';

declare let paypal: any;

@Component({
  selector: 'app-cenovnik',
  templateUrl: './cenovnik.component.html',
  styleUrls: ['./cenovnik.component.css']
})
export class CenovnikComponent implements AfterViewChecked {
  info : number;
  username:string;
  role:string;
  pritisnuto :boolean;
  cenaEuri: number;
  //public payPalConfig ? : IPayPalConfig;
  dobavljanjePayPal: any;
  karta : number = 0;

  constructor(private cenovnikService : CenovnikService,private router:Router) { }

  ngOnInit() {
this.username=localStorage.getItem('username');
this.role=localStorage.getItem('role');
this.pritisnuto=false;
//this.cenovnikMo = new CenovnikMo(0,this.username);
  }

  ShowCena(vrstaKarte : number){
    this.pritisnuto=true;
    this.karta = vrstaKarte;
    this.cenovnikService.getCenovnik(vrstaKarte,this.username).subscribe(data => {
      console.log(data);
      this.info = data;
      this.finalAmount = data;
      //this.finalAmount = this.finalAmount/118;
    });
    
  }

  kupiKartu(vrstaKarte : number)
  {
    this.cenovnikService.kupiKartu(vrstaKarte,this.username).subscribe(data => {
      console.log(data);
    });
    this.router.navigate(['/karteKorisnik']);
  }

  payPalfunkcija()
  {

    let payPalMod = new PayPalModel();
    payPalMod.PayementId = this.dobavljanjePayPal.id;
    let pom = new Date(this.dobavljanjePayPal.create_time);
    //pom.setHours(pom.getHours() + 2);
    payPalMod.CreateTime = pom;
    payPalMod.PayerEmail = this.dobavljanjePayPal.payer.payer_info.email;
    payPalMod.PayerName = this.dobavljanjePayPal.payer.payer_info.first_name;
    payPalMod.PayerSurname = this.dobavljanjePayPal.payer.payer_info.last_name;
    payPalMod.CurrencyCode = this.dobavljanjePayPal.transactions[0].amount.currency;
    payPalMod.Value = this.dobavljanjePayPal.transactions[0].amount.total;

    console.log("PayPal model: ", payPalMod);

    this.cenovnikService.addPayPal(payPalMod).subscribe(data => {
      this.cenovnikService.kupiKartuPayPal(this.karta,this.username,data).subscribe(data => {
       
        
          window.alert("Ticket successfully bought!")
          //ShowTicketsComponent.returned.next(false);
          //this.router.navigateByUrl('/show_tickets');
          this.router.navigate(['/karteKorisnik']);
        },
        err => {
          window.alert(err.error);
        })
  
       
      },
      err =>{
        window.alert(err.error)
        console.log(err);
      });

  }

  
  addScript: boolean = false;
  paypalLoad: boolean = true;
  
  finalAmount: number = 0;
 
  paypalConfig = {
    env: 'sandbox',
    client: {
      sandbox: 'AVaHdMpjQONSq6GOMXgWBSG8PGj1TDH7lGaAj8YpQpTrQw5RqBCqHRQO_-o47Xey1wBbnqhNYGDFHEU5',
      production: 'xxxxxxxxxx'
    },
    commit: true,
    payment: (data, actions) => {
      return actions.payment.create({
        payment: {
          transactions: [
            { amount: { total: this.finalAmount, currency: 'EUR' } }
          ]
        }
      });
    },
    onAuthorize: (data, actions) => {
      return actions.payment.execute().then((payment) => {
        //Do something when payment is successful.
        console.log("Uspesno!!");
        this.dobavljanjePayPal = payment;
        console.log(this.dobavljanjePayPal);
        this.payPalfunkcija();
      })
    }
  };
 
  ngAfterViewChecked(): void {
    if (!this.addScript) {
      this.addPaypalScript().then(() => {
        paypal.Button.render(this.paypalConfig, '#paypal-checkout-btn');
        this.paypalLoad = false;
      })
    }
  }
  
  addPaypalScript() {
    this.addScript = true;
    return new Promise((resolve, reject) => {
      let scripttagElement = document.createElement('script');    
      scripttagElement.src = 'https://www.paypalobjects.com/api/checkout.js';
      scripttagElement.onload = resolve;
      document.body.appendChild(scripttagElement);
    })
  }
}
