import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {RegistracijaServis} from 'src/app/servisi/registracija.servis';


@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {
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
