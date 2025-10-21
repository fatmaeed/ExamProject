import { Component, OnInit } from '@angular/core';
import { IResetPassword } from '../../models/ireset-password';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../services/account-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reset-password',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.css'
})
export class ResetPassword implements OnInit {

  constructor(private route: ActivatedRoute
    , private accountService: AccountService ,
    private router:Router
  ) { }
  resetPassModel: IResetPassword = { Email: '', Token: '', NewPassword: '', ConfirmNewPassword: '' }
  ngOnInit(): void {

    const email = this.route.snapshot.queryParamMap.get('email') || '';
    const token = decodeURIComponent(this.route.snapshot.queryParamMap.get('token') || '');
    if (email != '' && token != '') {
      this.resetPassModel.Email = email;
      this.resetPassModel.Token = token;
    }
  }

  resetPassForm = new FormGroup({
    NewPassword: new FormControl('', [Validators.required]),
    ConfirmNewPassword: new FormControl('', [Validators.required]),

  })


  get NewPassword() { return this.resetPassForm.controls["NewPassword"] }
  get ConfirmNewPassword() { return this.resetPassForm.controls["ConfirmNewPassword"] }


  submit() {

    if (this.resetPassForm.status == "VALID") {

      this.resetPassModel.NewPassword = this.resetPassForm.value.NewPassword || '';
      this.resetPassModel.ConfirmNewPassword = this.resetPassForm.value.ConfirmNewPassword || ''
      console.log(this.resetPassModel)
      this.accountService.ResetPassword(this.resetPassModel).subscribe({
        next: (res) => {
        alert(res.message)
          this.router.navigate(['/account/login'])

        },
        error: (err) => {
          console.log(err)

          if (err.error.hasOwnProperty('errors')) {
            for (const key in err.error.errors) {
              if (this.resetPassForm.get(key)) {
                this.resetPassForm.get(key)?.setErrors({ server: err.error.errors[key][0] });
              }
            }
          }else {
           
              if (this.resetPassForm.get("NewPassword")) {
                this.resetPassForm.get("NewPassword")?.setErrors({ server: "Please Make Password Contain Capital Letters And Special Characters" });
              }
            
          }
        }
      })
    } else {
      this.resetPassForm.markAllAsTouched();
      return;
    }
  }
}



