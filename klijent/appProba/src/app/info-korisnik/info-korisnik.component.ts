import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, Validators ,FormGroup} from '@angular/forms';
import { Router } from '@angular/router';
import {InfoService} from 'src/app/servisi/info.service';
import {RegistracijaModel} from 'src/app/model/registracijaModel';
import {PassengerTypeEnum} from 'src/app/model/enums';
import { RegistracijaServis } from '../servisi/registracija.servis';

@Component({
  selector: 'app-info-korisnik',
  templateUrl: './info-korisnik.component.html',
  styleUrls: ['./info-korisnik.component.css']
})
export class InfoKorisnikComponent implements OnInit {
  username : string;
  role:string;
  usriImage:string;
data:RegistracijaModel;
  user : RegistracijaModel = {
  Name : '',
  LastName : '',
  UserName : '',
  Email : '',
  Address : '',
  BirthdayDate : '',
  PassengerType : 1,
  Password : '',
  ConfirmPassword : '',
  Document : '',
};

infoForm = this.fb.group({
    Name : [''],
    LastName : [''],
    UserName : ['', Validators.required],
    Email : ['', Validators.required],
    Address : [''],
    BirthdayDate : [''],
    PassengerType : [''],
    Password : ['', Validators.required],
    ConfirmPassword : ['', Validators.required],
    Document : [],
  });
 
  constructor(private fb : FormBuilder, private registracijaServis : InfoService, private router:Router,private userService : RegistracijaServis) { }

  ngOnInit() {
    this.username = localStorage.getItem('username');
    this.role = localStorage.getItem('role');
    this.registracijaServis.getInfo(this.username).subscribe(data => {
      console.log(data);
      this.user = data;
      
      //this.slika = data.Document.split
      //this.user.Document = this.slika[2];
      this.infoForm.patchValue(data);
    });
   
     
 
  }
  onchange()
  {
  
  }
  onSubmit()
  {
    this.user = this.infoForm.value;
    this.user.Document=this.usriImage;
      console.log(this.user);
      this.registracijaServis.postChangedInfo(this.user).subscribe(data => {
        console.log('Uspesno izmenjen cenovnik!');
      });
    /*this.registracijaServis.getInfo(this.username).subscribe(data => {
    console.log(data);
    this.user = data;
    this.registerForm.patchValue(data);
 
    this.user = this.registerForm.value;
  });*/
    /*console.log(this.user);
    this.registracijaServis.postChangedInfo(this.user).subscribe(data => {
      console.log('Uspesno izmenje informacije o korisniku!');
    });*/
  }

  onFileChanged(event) {
    if (event.target.files && event.target.files[0]) {
  
      const file = event.target.files[0];
  
      const reader = new FileReader();
      reader.onload = e => {this.user.Document = reader.result.toString().split(',')[1]; 
      console.log(this.user.Document);this.usriImage=this.user.Document };
    
  
      reader.readAsDataURL(file);
      console.log(file);
     // this.user.Document = file;
    }
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

