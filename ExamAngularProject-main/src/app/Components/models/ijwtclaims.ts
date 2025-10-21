import { jwtDecode } from "jwt-decode";

export interface IJWTClaims {
    id:string ,
    userName:string ,
    role:string
}

