import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, Validators ,FormGroup} from '@angular/forms';
import { Router } from '@angular/router';
import {InfoService} from 'src/app/servisi/info.service';
import {RegistracijaModel} from 'src/app/model/registracijaModel';
import {PassengerTypeEnum} from 'src/app/model/enums';

@Component({
  selector: 'app-info-korisnik',
  templateUrl: './info-korisnik.component.html',
  styleUrls: ['./info-korisnik.component.css']
})
export class InfoKorisnikComponent implements OnInit {

  @Input() user : RegistracijaModel = {
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

registerForm = this.fb.group({
    Name : [this.user.Name],
    LastName : [this.user.LastName],
    UserName : [this.user.UserName, Validators.required],
    Email : [this.user.Email, Validators.required],
    Address : [this.user.Address],
    BirthdayDate : [this.user.BirthdayDate],
    PassengerType : [this.user.PassengerType],
    Password : [this.user.Password, Validators.required],
    ConfirmPassword : [this.user.ConfirmPassword, Validators.required],
    Document : [this.user.Document],
  });
  constructor(private fb : FormBuilder, private registracijaServis : InfoService, private router:Router) { }

  ngOnInit() {
    this.registracijaServis.getInfo(localStorage.getItem('username')).subscribe(user => { this.user = user
    

   /*this.registerForm.patchValue = ({
      Name : user.Name,
      LastName : '',
      UserName : '',
      Email : '',
      Address : '',
      BirthdayDate : '',
      PassengerType : 1,
      Password : '',
      ConfirmPassword : '',
      Document : '',});*/

  });
  }
  onchange()
  {
  
  }
  onSubmit()
  {
   /* console.log(this.registerForm.value);
    this.registracijaServis.register(this.registerForm.value).subscribe(data => {
      console.log('Registration successfully done.');
      this.router.navigate(['/']);
    })*/;
  }

  onFileChange(event)
  {
    
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
}

