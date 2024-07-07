import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../Model/User';
import { Observable } from 'rxjs';
import { ChangeUserRole } from '../Model/ChangeUserRole';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  //בנאי
  // JWT קבלת אוביקט משתמש על פי
  // JWT קבלת מזהה משתמש על פי
  // הוספת משתמש
  // קבלת משתמש לפי מזהה
  // קבלת כל המשתמשים מוגבל למהל
  // קבלת כל המנהלים מוגבל למנהל
  // קבלת משתמש על פי מייל
  // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
  // JWT עדכון משתמש על פי
  // JWT מחיקת משתמש על פי
  // מחיקת משתמש למנהל
  // עדכון תפקיד למשתמש מוגבל למנהל
  // האם קיים מייל כזה
  //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
  //קבלת כמה אחוז משתמשים יוזרים קלאסים
endPointApi="https://localhost:44391/api/User/";
//בנאי
constructor(private http:HttpClient) { }

        // JWT קבלת אוביקט משתמש על פי
        async GetUserByToken(Token:string):Promise<Observable<User>>
        {
          return this.http.get<User>(this.endPointApi+"GetUserByToken",{
            headers:new HttpHeaders().set('Authorization',""+Token)});
        }

        // JWT קבלת מזהה משתמש על פי
        async GetUserIdByToken(Token:string):Promise<Observable<number>>
        {
          return this.http.get<number>(this.endPointApi+"GetUserIdByToken",{
            headers:new HttpHeaders().set('Authorization',""+Token)});
        }

        // הוספת משתמש
        async AddUser(UserObjToAdd:User):Promise<Observable<string>>
        {
          return this.http.post<string>(this.endPointApi+"AddUser",UserObjToAdd);
        }

        // קבלת משתמש לפי מזהה
        async GetUserById(id:number):Promise<Observable<User>>
        {
          return this.http.get<User>(this.endPointApi+"GetUserById/"+id);
        }

       // קבלת כל המשתמשים מוגבל למהל
        async GetAllUsers(Token:string):Promise<Observable<Array<User>>>
        {
          return this.http.get<Array<User>>(this.endPointApi+"GetAllUsers",{
            headers:new HttpHeaders().set('Authorization',""+Token)});
        }

        // קבלת כל המנהלים מוגבל למנהל
        async GetAllAdmin(Token:string):Promise<Observable<Array<User>>>
        {
          return this.http.get<Array<User>>(this.endPointApi+"GetAllAdmin",{
            headers:new HttpHeaders().set('Authorization',""+Token)});
        }

        // קבלת משתמש על פי מייל
        async GetUserByMail(Mail:string):Promise<Observable<User>>
        {
          return this.http.get<User>(this.endPointApi+"GetUserByMail/"+Mail);
        }

        // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
        async GetAllUserNotAdmin(Token:string):Promise<Observable<Array<User>>>
        {
          return this.http.get<Array<User>>(this.endPointApi+"GetAllUserNotAdmin",{
            headers:new HttpHeaders().set('Authorization',""+Token)});
        }

        // JWT עדכון משתמש על פי
        async UpdateUserByJWT(Token:string,UserObjToUpdate:User):Promise<Observable<any>>
        {
          return this.http.put<any>(this.endPointApi+"UpdateUserByJWT",UserObjToUpdate,{
            headers:new HttpHeaders().set('Authorization',""+Token)
          });
        }

        // JWT מחיקת משתמש על פי
        async DeleteUserByJWT(Token:string):Promise<Observable<any>>
        {
          return this.http.delete<any>(this.endPointApi+"DeleteUserByJWT",{
            headers:new HttpHeaders().set('Authorization',""+Token)
          });
        }

        // מחיקת משתמש למנהל
        async DeleteUserByIdForAdmin(Token:string,Id:number):Promise<Observable<any>>
        {
          return this.http.delete<any>(this.endPointApi+"DeleteUserByIdForAdmin/"+Id,{
            headers: new HttpHeaders().set('Authorization',""+Token)
          });
        }

        // עדכון תפקיד למשתמש מוגבל למנהל
        async ChangeUserRoleForAdmin(Token:string,UserToUpdate:ChangeUserRole):Promise<Observable<any>>
        {
          return this.http.put<any>(this.endPointApi+"ChangeUserRoleForAdmin",UserToUpdate,{
            headers:new HttpHeaders().set('Authorization',""+Token)
          });
        }

        // האם קיים מייל כזה
        async GetHaveUser(Mail:string):Promise<Observable<boolean>>
        {
          return this.http.get<boolean>(this.endPointApi+"GetHaveUser/"+Mail);
        }

        //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
        async ForgotPassword(Mail:string):Promise<Observable<boolean>>
        {
          return this.http.get<boolean>(this.endPointApi+"ForgotPassword/"+Mail);
        }

        //קבלת כמה אחוז משתמשים יוזרים קלאסים
        async GetAllPrecentageClassicUser(Token:string):Promise<Observable<number>>
        {
          return this.http.get<number>(this.endPointApi+"GetAllPrecentageClassicUser",{
            headers:new HttpHeaders().set('Authorization',""+Token)
          });
        }

}
