import { Component, OnInit } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { IJWTClaims } from '../../models/ijwtclaims';
import { AccountService } from '../../services/account-service';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {

  constructor(private accountService:AccountService) {}
  ngOnInit(): void {
   const token = localStorage.getItem('token'); 

if (token) {
  const decoded = jwtDecode<IJWTClaims>(token);

}
  }


  logOut() {
    this.accountService.logout() ;
  }
}
