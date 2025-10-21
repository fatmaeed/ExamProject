import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../services/account-service';
import { Router, RouterLink } from '@angular/router';
import { ILoginUser } from '../../models/ILoginUser';
import { jwtDecode } from 'jwt-decode';
import { IJWTClaims } from '../../models/ijwtclaims';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule , RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login   {

role:string = '';
  loginModel!:ILoginUser
constructor(private accountService:AccountService , private routerObj: Router){}

loginForm = new FormGroup({
  UserName: new FormControl('',[Validators.required]) ,
  Password : new FormControl('' , [Validators.required]) ,

})

get UserName() {return this.loginForm.controls['UserName']}
get Password() {return this.loginForm.controls['Password']}

isServerErrors:boolean = false ;
errorMessage:string = ''

submit() {


   if(this.loginForm.status == "VALID") {

    this.loginModel = {...this.loginForm.value} as ILoginUser
   this.accountService.login(this.loginModel).subscribe({
      next: (res) => {

        localStorage.setItem('token', res.token);
         const token = res.token

     if (token) {
      const decoded = jwtDecode<IJWTClaims>(token);
       this.role = decoded.role;
     }
     if(this.role == 'admin') {
        this.routerObj.navigate(['/examlist']);
      }else if(this.role == 'student') {
        this.routerObj.navigate(['/home']);
      }
      },
      error: (err) => {
         this.loginForm.get("Password")?.setErrors({ server: err.error.message});
         this.loginForm.get("UserName")?.setErrors({ server: err.error.message});


      }
    }); ;
   }else {
      this.loginForm.markAllAsTouched()
  return;
   }

}

}
