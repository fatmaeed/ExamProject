import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../services/account-service';
import { IChangePassword } from '../../models/ichange-password';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './change-password.html',
  styleUrl: './change-password.css'
})
export class ChangePassword {
  changPassModel!: IChangePassword
  constructor(private accountService: AccountService, private router: Router) { }
  changePassForm = new FormGroup({
    CurrentPassword: new FormControl('', [Validators.required]),
    NewPassword: new FormControl('', [Validators.required]),
    ConfirmNewPassword: new FormControl('', [Validators.required]),
  })

  get CurrentPassword() { return this.changePassForm.controls['CurrentPassword'] }
  get NewPassword() { return this.changePassForm.controls['NewPassword'] }
  get ConfirmNewPassword() { return this.changePassForm.controls['ConfirmNewPassword'] }


  submit() {

    if (this.changePassForm.status == "VALID") {

      this.changPassModel = { ...this.changePassForm.value } as IChangePassword
      this.accountService.changePassword(this.changPassModel).subscribe({
        next: (res) => {
          alert(res.message)
          this.router.navigate(['/home']); 
        },
        error: (err) => {
          if (err.status == 401) {
            this.router.navigate(['/account/login'])
          }
          if (err.error.hasOwnProperty('errors')) {
            for (const key in err.error.errors) {
              if (this.changePassForm.get(key)) {
                this.changePassForm.get(key)?.setErrors({ server: err.error.errors[key][0] });
              }
            }
          }else {
            for (const key in err.error) {
              if (this.changePassForm.get(key)) {
                this.changePassForm.get(key)?.setErrors({ server: err.error[key][0] });
              }
            }
          }
        }
      });;
    } else {
      this.changePassForm.markAllAsTouched()
      return;
    }
  }
}
