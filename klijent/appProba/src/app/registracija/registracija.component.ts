import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {RegistracijaServis} from 'src/app/servisi/registracija.servis';
import {RegistracijaModel} from 'src/app/model/registracijaModel';
import {PassengerType} from 'src/app/model/enums';

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {

user : RegistracijaModel = {
  Name : '',
  LastName : '',
  UserName : '',
  Email : '',
  Address : '',
  BirthdayDate : '',
  PassengerType : PassengerType.Regularan,
  Password : '',
  ConfirmPassword : '',
};

registerForm = this.fb.group({
    Name : [''],
    LastName : [''],
    UserName : ['', Validators.required],
    Email : ['', Validators.required],
    Address : [''],
    BirthdayDate : [''],
    PassengerType : [''],
    Password : ['', Validators.required],
    ConfirmPassword : ['', Validators.required]
  });
  constructor(private fb : FormBuilder, private registracijaServis : RegistracijaServis, private router:Router) { }

  ngOnInit() {
    this.user.PassengerType = PassengerType.Regularan;
  }
  onchange()
  {
    
  }

  onSubmit()
  {
    console.log(this.registerForm.value);
    this.registracijaServis.register(this.registerForm.value).subscribe(data => {
      console.log('Registration successfully done.');
      this.router.navigate(['/log-in']);
    });
  }
}
