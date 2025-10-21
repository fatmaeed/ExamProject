import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AccountService } from '../../services/account-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-confirm-email',
  imports: [CommonModule],
  templateUrl: './confirm-email.html',
  styleUrl: './confirm-email.css'
})
export class ConfirmEmail implements OnInit {

  
  constructor(
    private route: ActivatedRoute,
    private accountService:AccountService,
    private router: Router
  ) {}
  showBody:boolean =true
  ngOnInit() {
    const userId = this.route.snapshot.queryParamMap.get('userId') || '';
    const token = decodeURIComponent( this.route.snapshot.queryParamMap.get('token') || ''  );
     if(userId != '' && token != '') {
        this.showBody = false
       this.accountService.confirmEmail(userId,token).subscribe({
      next: (res) => {
        localStorage.setItem('token', res.signInToken); 
        this.router.navigate(['/home']); 
      },
      error: (err) => {
        console.error('Email confirmation failed:', err);
      }
    });
     }
   
  }

}
