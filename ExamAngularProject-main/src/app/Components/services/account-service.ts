import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegisterUser } from '../models/IRegisterUser';
import { catchError, Observable, throwError } from 'rxjs';
import { ILoginUser } from '../models/ILoginUser';
import { Route, Router } from '@angular/router';
import { IChangePassword } from '../models/ichange-password';
import { IForgotPassword } from '../models/iforgot-password';
import { IResetPassword } from '../models/ireset-password';
import { jwtDecode } from 'jwt-decode';
import { IJWTClaims } from '../models/ijwtclaims';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl:string = 'https://localhost:7085/api/Account'

  constructor(private http:HttpClient , private router:Router) { }

registerUser(user:IRegisterUser) : Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.baseUrl}/register`,user).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 400) {

        const validationErrors = error.error;
        return throwError(() => validationErrors);
      } else {

        return throwError(() => 'Something went wrong. Please try again.');
      }
    })
  );  ;
}

confirmEmail(userId:string,token:string) {
  return this.http.get<{ signInToken: string }>(
      `'https://localhost:7085/api/Account/ConfirmEmail?userId=${userId}&token=${token}`
    );
}

login(user: ILoginUser): Observable<{ token: string; userId: string }> {
  return this.http.post<{ token: string; userId: string }>(`${this.baseUrl}/login`, user);
}

isLoggedIn(): boolean {
  return !!localStorage.getItem('token');
}

getUserId(): string | null {
  return localStorage.getItem('userId');
}

logout(): void {
  localStorage.removeItem('token');
  localStorage.removeItem('userId');
  this.router.navigate(['/account/login']);
}

changePassword(changePassModel:IChangePassword) : Observable<{message:string}> {
return this.http.post<{message:string}>(`${this.baseUrl}/ChangePassword` , changePassModel)

}

forgotPassword(forgotPassModel:IForgotPassword) : Observable<{message:string}> {
  return this.http.post<{message:string}>(`${this.baseUrl}/ForgotPassword` , forgotPassModel);
}


ResetPassword(resetPassModel:IResetPassword) : Observable<{message:string}> {
  return this.http.post<{message:string}>(`${this.baseUrl}/ResetPassword` , resetPassModel);
}
getUserIdFromToken(): string | null {
  const token = localStorage.getItem('token');
  if (!token) return null;

  try {
    const decoded = jwtDecode<IJWTClaims>(token);
    return decoded.id;
  } catch (e) {
    console.error('Invalid token', e);
    return null;
  }
}
}


