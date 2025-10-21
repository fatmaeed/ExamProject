import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { IJWTClaims } from '../models/ijwtclaims';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = localStorage.getItem('token');

    if (token) {
      try {
        const decoded = jwtDecode<IJWTClaims>(token);

        if (decoded.role === 'admin') {
          this.router.navigate(['/not-found']);
          return false;
        }

        return true;
      } catch (error) {
        this.router.navigate(['/account/login']);
        return false;
      }
    }

    this.router.navigate(['/account/login']);
    return false;
  }
}
