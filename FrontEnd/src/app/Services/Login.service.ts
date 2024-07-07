import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../Model/Login';
import { Observable } from 'rxjs';
import { User } from '../Model/User';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
//תקציר
////////
//בנאי
//הזדאות וקבלת תוקן
//תוקן
//רוול
//נאב

endPointApi="https://localhost:44391/api/User/";

    //בנאי
constructor(private http:HttpClient) { }

  //הזדאות וקבלת תוקן
  async auth(auth:Login):Promise<Observable<string>>
  {
    return this.http.post(this.endPointApi+"auth",auth,{responseType: 'text' });
  }

  //הזדאות וקבלת יוזר
  async getUser(Token:string):Promise<Observable<User>>
  {
    return this.http.get<User>(this.endPointApi+"GetUserByToken",{
      headers:new HttpHeaders().set('Authorization',""+Token)
    });
  }

  //תוקן
  get Token():string
  {
    return String(window.sessionStorage.getItem("Token"));
  }
  set Token(val:string)
  {
    window.sessionStorage.setItem("Token",val);
  }

  //רוול
  get Role():string
  {
    return String(window.sessionStorage.getItem("Role"));
  }
  set Role(val:string)
  {
    window.sessionStorage.setItem("Role",val);
  }

  //נאב
  get Navbar():string
  {
    return String(window.sessionStorage.getItem("Navbar"));
  }
  set Navbar(val:string)
  {
    window.sessionStorage.setItem("Navbar",val);
  }

}
