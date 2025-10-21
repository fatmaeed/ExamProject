import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../services/account-service';
import { Router, RouterLink } from '@angular/router';
import { IRegisterUser } from '../../models/IRegisterUser';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule , CommonModule , RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
registerModel!:IRegisterUser
constructor(private accountService:AccountService , private routerObj: Router){}
registerForm = new FormGroup({
  username: new FormControl('',[Validators.required]) ,
  email: new FormControl('',[Validators.required , Validators.email]) ,
  phoneNumber: new FormControl('',[Validators.required, Validators.pattern('^01[0-9]{9}$')]) ,
  password : new FormControl('' , [Validators.required]) ,
  confirmpassword : new FormControl('' , [Validators.required]) ,

})
get userName(){return this.registerForm.controls["username"]} ;
get email(){return this.registerForm.controls["email"]} ;
get phoneNumber(){return this.registerForm.controls["phoneNumber"]} ;
get password(){return this.registerForm.controls["password"]} ;
get confirmPassword(){return this.registerForm.controls["confirmpassword"]} ;
submitted = false;


sumbit() {

this.submitted = true ;
 if(this.registerForm.status == "VALID") {
  this.registerModel = {...this.registerForm.value} as IRegisterUser
  this.accountService.registerUser(this.registerModel).subscribe({
  next: () => {
    alert("Please Check Your Email To Confirm Account Before Login");
    this.routerObj.navigate(['/account/login'])

  },
  error: (errors) => {


    if (typeof errors === 'object') {
      this.mapServerErrors(errors.errors);
    }
     if(errors?.length > 0) {
        this.registerForm.get("password")?.setErrors({ server: "Please Make Password Contain Capital Letters And Special Characters"});
    }
  }
});
 }else  {
  this.registerForm.markAllAsTouched()
  return;

 }

}

mapServerErrors(errors: { [key: string]: string[] }) {
  for (const key in errors) {

    if (this.registerForm.get(key.toLowerCase())) {
      this.registerForm.get(key.toLowerCase())?.setErrors({ server: errors[key][0] });
    }

  }
}

}
