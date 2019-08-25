import { Component, OnInit } from '@angular/core';
import{AuthService} from 'src/app/auth/auth.service';
import{Router} from '@angular/router';

import { from } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  username:string;
  role:string;
  constructor(private userService:AuthService,private router:Router ) { }

  ngOnInit() {
    this.username = localStorage.getItem('username');
    this.role = localStorage.getItem('role');
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
