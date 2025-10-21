import { ResetPassword } from './../../Components/Account/reset-password/reset-password';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../Components/services/account-service';
import { IChangePassword } from '../../Components/models/ichange-password';

@Component({
  selector: 'app-admin-side-bar',
  imports: [],
  templateUrl: './admin-side-bar.html',
  styleUrl: './admin-side-bar.css'
})
export class AdminSideBar {
@Input() adminName: string = 'Admin';

  constructor(public accountService: AccountService,private router: Router) {}
goToExamList() {
    this.router.navigate(['/examlist']);
}
 logout(): void {
    this.accountService.logout();
  }
 changePassword(): void {
    this.router.navigate(['/account/changepassword']);

  }

}

