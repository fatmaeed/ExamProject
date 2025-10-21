import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AccountService } from '../../Components/services/account-service';
import { Router, RouterLink } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { IJWTClaims } from '../../Components/models/ijwtclaims';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css'
})
export class NavBar implements OnInit {
  userName!:string
constructor(public accountService: AccountService, private router: Router , private cdr:ChangeDetectorRef) {}
  ngOnInit(): void {
    let token = localStorage.getItem('token');
     if (token) {
      const decoded = jwtDecode<IJWTClaims>(token);
       this.cdr.detectChanges();
       this.userName = decoded.userName ;

     }
  }
  goToSection(sectionId: string) {
  const element = document.getElementById(sectionId);
  if (element) {
    element.scrollIntoView({ behavior: 'smooth', block: 'start' });
  }
}

  logout(): void {
    this.accountService.logout();
  }

  goToProfile(): void {
    this.router.navigate(['profile']);
  }

  goToLogin(): void {
    this.router.navigate(['/account/login']);
  }

 changePassword(): void {
    this.router.navigate(['/account/changepassword']);

  }
}
