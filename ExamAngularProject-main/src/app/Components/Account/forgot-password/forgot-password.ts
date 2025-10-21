import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { EmailValidator, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../services/account-service';
import { IForgotPassword } from '../../models/iforgot-password';

@Component({
  selector: 'app-forgot-password',
  imports: [ReactiveFormsModule , CommonModule],
  templateUrl: './forgot-password.html',
  styleUrl: './forgot-password.css'
})
export class ForgotPassword {

constructor(private accountService:AccountService) {}

forgotPassForm = new FormGroup({
    Email: new FormControl('',[Validators.required , Validators.email]) ,

})
get Email() {return this.forgotPassForm.controls["Email"]}
forgotPassModel!:IForgotPassword

submit() {

  if(this.forgotPassForm.status == "VALID") {
     this.forgotPassModel = {...this.forgotPassForm.value} as IForgotPassword
       this.accountService.forgotPassword(this.forgotPassModel).subscribe({
          next: (res) => {
            alert("Please Check Your Email To Reset Your Password")
           console.log(res)
          },
          error: (err) => {
            console.log(err)
         this.forgotPassForm.get("Email")?.setErrors({ server: err.error.errors.Email[0]});
           
          }
        }); ;

   
  }else {
    this.forgotPassForm.markAllAsTouched() ;
    return;
  }
}
}
