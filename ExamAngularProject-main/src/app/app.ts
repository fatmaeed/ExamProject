import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, RouterOutlet } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { IJWTClaims } from './Components/models/ijwtclaims';
import { AdminSideBar } from "./Pages/admin-side-bar/admin-side-bar";
import { NavBar } from "./layout/nav-bar/nav-bar";
import { Footer } from "./layout/footer/footer";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
  imports: [AdminSideBar, NavBar, RouterOutlet, Footer],
})
export class App implements OnInit {
  role = '';
  adminName = '';
  isNotFoundPage = false;

  constructor(private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decoded = jwtDecode<IJWTClaims>(token);
        this.role = decoded.role;
        this.adminName = decoded.userName;
      } catch {
        localStorage.removeItem('token');
        this.router.navigate(['/account/login']);
      }
    }

    // Detect if current page is 'notFound'
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.isNotFoundPage = this.router.url.includes('/notFound') || this.router.url === '/404';
      }
    });
  }
}
