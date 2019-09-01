import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import {RegistracijaServis} from 'src/app/servisi/registracija.servis';
import { LoginModel } from 'src/app/model/login-model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  message: string;

  user : LoginModel = {
    Username: '',
    Password: '',
  };

  loginForm = this.fb.group({
    Username: ['', Validators.required],
    Password: ['', Validators.required],
  });

  constructor(public authService: RegistracijaServis, public router: Router, private fb: FormBuilder) {
   // this.setMessage();
  }

 /* setMessage() {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');
    //this.router.navigate(['/']);
  }*/

  login() {
    this.user = this.loginForm.value;
    console.log(this.user);
    this.authService.logIn(this.user);
    
    this.router.navigate(['/']);
    
   
  
  
  }

 

  logout() {
    this.authService.logout();
   // this.setMessage();
  }
}
