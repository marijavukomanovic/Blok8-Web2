import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {LoginService} from 'src/app/servisi/login.service';
import { LoginModel } from 'src/app/model/login-model';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  loginForm=this.fb.group({
    UserName : ['', Validators.required],
    password : ['', Validators.required]
  });

  
  constructor(private fb : FormBuilder, private loginService: LoginService) { }

  ngOnInit() {

    
  }

  onSubmit()
  {
    this.loginService.login(this.loginForm.value as LoginModel).subscribe(data => 
    {
      console.log(data)
    });
  }

}
