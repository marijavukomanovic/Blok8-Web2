import { Component, OnInit, AfterViewChecked, Input } from '@angular/core';

 
declare let paypal: any;

@Component({
  selector: 'app-paypal-proba',
  templateUrl: './paypal-proba.component.html',
  styleUrls: ['./paypal-proba.component.css']
})

  export class PaypalProbaComponent implements OnInit 
  {

    ngOnInit() {
    }
    // @Input()
    // public cena: number;
  
    // addScript: boolean = false;
    // paypalLoad: boolean = true;
    
    // finalAmount: number = this.cena;
   
    // paypalConfig = {
    //   env: 'sandbox',
    //   client: {
    //     sandbox: 'marko_srb-facilitator@hotmail.rs',
    //     production: 'access_token$sandbox$rxmzt3yrz365v9cy$c304ff9fac12f48c4426b7697aeb2550'
    //   },
    //   commit: true,
    //   payment: (data, actions) => {
    //     return actions.payment.create({
    //       payment: {
    //         transactions: [
    //           { amount: { total: this.finalAmount, currency: 'INR' } }
    //         ]
    //       }
    //     });
    //   },
    //   onAuthorize: (data, actions) => {
    //     return actions.payment.execute().then((payment) => {
    //       alert("Uspesno ste obavili placanje!");
    //     })
    //   }
    // };
   
    // ngAfterViewChecked(): void {
    //   if (!this.addScript) {
    //     this.addPaypalScript().then(() => {
    //       paypal.Button.render(this.paypalConfig, '#paypal-checkout-btn');
    //       this.paypalLoad = false;
    //     })
    //   }
    // }
    
    // addPaypalScript() {
    //   this.addScript = true;
    //   return new Promise((resolve, reject) => {
    //     let scripttagElement = document.createElement('script');    
    //     scripttagElement.src = 'https://www.paypalobjects.com/api/checkout.js';
    //     scripttagElement.onload = resolve;
    //     document.body.appendChild(scripttagElement);
    //   })
    // }
  
  
    private loadExternalScript(scriptUrl: string) {
      return new Promise((resolve, reject) => {
        const scriptElement = document.createElement('script')
        scriptElement.src = scriptUrl
        scriptElement.onload = resolve
        document.body.appendChild(scriptElement)
    })}
  
    ngAfterViewInit(): void {
      this.loadExternalScript("https://www.paypalobjects.com/api/checkout.js").then(() => {
        paypal.Button.render({
          env: 'sandbox',
          client: {
            production: 'xxxxxxxxxx',
            sandbox: 'AVaHdMpjQONSq6GOMXgWBSG8PGj1TDH7lGaAj8YpQpTrQw5RqBCqHRQO_-o47Xey1wBbnqhNYGDFHEU5'
            
          },
          commit: true,
          payment: function (data, actions) {
            return actions.payment.create({
              payment: {
                transactions: [
                  {
                    amount: { total: '1.00', currency: 'USD' }
                  }
                ]
              }
            })
          },
          onAuthorize: function(data, actions) {
            return actions.payment.execute().then(function(payment) {
              // TODO

              console.log("STIGLII SMOOOOOOOOO");
            })
          }
        }, '#paypal-button');
      });
    }
   
  }