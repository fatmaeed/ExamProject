import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AccountService } from '../../Components/services/account-service';
import { Router } from '@angular/router';
import { IJWTClaims } from '../../Components/models/ijwtclaims';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-hero-section',
  imports: [],
  templateUrl: './hero-section.html',
  styleUrl: './hero-section.css'
})
export class HeroSection implements OnInit {
constructor(public accountService: AccountService, private router: Router , private cdr:ChangeDetectorRef) {}
  ngOnInit(): void {
    let token = localStorage.getItem('token');
     if (token) {
      const decoded = jwtDecode<IJWTClaims>(token);
       this.cdr.detectChanges();
     }
  }
   goToRegister(): void {
    this.router.navigate(['/account/register']);
  }
}
