import { Injectable } from '@angular/core';
import {
  CanActivate, Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanActivateChild,
} from '@angular/router';

@Injectable({
  providedIn: 'root',
})

// ovaj guard ce se koristiti za sve putanje kojima moze da pristupi samo AppUser
export class ControllerGuard implements CanActivate, CanActivateChild {
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {    
    if (localStorage.role === 'Controller') {
      return true;
    }
    // not logged in so redirect to login page
    else {
      console.error("Can't access, not controller");
      if (localStorage.getItem('username') === null) {
        this.router.navigate(['/login']);
      } else {
        this.router.navigate(['/']);
      }
      return false;
    }
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.canActivate(route, state);
  }

}
