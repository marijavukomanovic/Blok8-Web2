import { Component, OnInit } from '@angular/core';
import { InfoModel } from '../model/registracijaModel';
import { FormBuilder } from '@angular/forms';
import { InfoService } from '../servisi/info.service';
import { Router } from '@angular/router';
import { RegistracijaServis } from '../servisi/registracija.servis';
import { KontrolerService } from '../servisi/kontroler.service';

@Component({
  selector: 'app-kontroler-korisnici',
  templateUrl: './kontroler-korisnici.component.html',
  styleUrls: ['./kontroler-korisnici.component.css']
})
export class KontrolerKorisniciComponent implements OnInit {

  username : string;
  role:string;
  usriImage:string;
  korisnici : Array<string>;
  statuses : Array<string>;
  info :string;
  pritisnuto : boolean;

data:InfoModel;
  user : InfoModel = {
  Name : '',
  LastName : '',
  UserName : '',
  Email : '',
  Address : '',
  BirthdayDate : '',
  PassengerType : 1,
  StatusVerifikacije : '',
  Document : '',
};

/*infoForm = this.fb.group({
  Korisnici : [''],
    Name : [''],
    LastName : [''],
    UserName : [''],
    Email : [''],
    Address : [''],
    BirthdayDate : [''],
    PassengerType : [''],
    StatusVerifikacije : [''],
    Document : [],
  });*/
 
  constructor(private fb : FormBuilder, private kontrolerServis : KontrolerService, private router:Router,private userService : RegistracijaServis) { }

  ngOnInit() {
    this.pritisnuto = false;
    this.username = localStorage.getItem('username');
    this.role = localStorage.getItem('role');
    this.info = "";

    this.kontrolerServis.getUseri(this.username).subscribe(data => {
      console.log(data);
      this.korisnici = data;
      
    });

  }
  
  getKorisnik(user:string){
    console.log(user);
    this.pritisnuto = true;
  this.kontrolerServis.getInfo(user).subscribe(data => {
    //console.log(data);
    this.user = data;
    if(this.user.PassengerType == 1)
    {
        this.info = "Student";
    }
    else if(this.user.PassengerType == 2)
    {
        this.info = "Penzioner";
    }
    else if(this.user.PassengerType == 3)
    {
      this.info = "Regularan";
    }
    //console.log(this.info);
    
    //this.infoForm.patchValue(data);
    this.statuses = ["Obrada","Verifikovan","Odbijen"];
  });
}


  zavrsi(StatusVerifikacije : string)
  {
    this.kontrolerServis.saljiStatus(this.user.UserName,StatusVerifikacije).subscribe(data => {
      console.log(data);
      console.log('Uspesno izvrsena verifikacija!');
      this.pritisnuto = false;
    });
  }
 

 onClickLogout(event: Event): void {
  event.preventDefault(); // Prevents browser following the link
  // Here you can call your service method to logout the user
  // and then redirect with Router object, for example
  this.username = '';
  this.role = '';
  this.userService.logout();
  this.router.navigate(['login']);
}
}