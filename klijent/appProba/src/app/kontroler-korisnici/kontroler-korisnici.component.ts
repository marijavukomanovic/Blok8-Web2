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

infoForm = this.fb.group({
    Name : [''],
    LastName : [''],
    UserName : [''],
    Email : [''],
    Address : [''],
    BirthdayDate : [''],
    PassengerType : [''],
    StatusVerifikacije : [''],
    Document : [],
  });
 
  constructor(private fb : FormBuilder, private kontrolerServis : KontrolerService, private router:Router,private userService : RegistracijaServis) { }

  ngOnInit() {
    this.username = localStorage.getItem('username');
    this.role = localStorage.getItem('role');


    this.kontrolerServis.getUseri(this.username).subscribe(data => {
      console.log(data);
      this.korisnici = data;
      
    });

  }
  
  getKorisnik(korisnic : string){
  this.kontrolerServis.getInfo(korisnic).subscribe(data => {
    console.log(data);
    this.user = data;
    
    this.infoForm.patchValue(data);
    this.statuses = ["Obrada","Verifikovan","Odbijen"];
  });
}

  onSubmit()
  {
    this.user = this.infoForm.value;
    this.user.Document=this.usriImage;
      this.kontrolerServis.saljiStatus(this.user.UserName,this.user.StatusVerifikacije).subscribe(data => {
        console.log('Uspesno izvrsena verifikacija!');
      });
    
  }

  ownerLevels = [
    { id: 1, name: 'Student' },
    { id: 2, name: 'Penzioner' },
    { id: 3, name: 'Regularan' }
 ];
 
 selectedOwnerLevel: number = 0;
 
 onChangeOwnerLevel(ownerLevelId: number) {
    this.selectedOwnerLevel = ownerLevelId;
    this.user.PassengerType = ownerLevelId;
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