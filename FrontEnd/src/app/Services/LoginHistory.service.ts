import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginHistory } from './../Model/LoginHistory';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginHistoryService {
//תקציר
//בנאי
//הוספת היסטוריה
//מחיקת היסטוריה לפי מזהה היסטוריה
//קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
//קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
//קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
//קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
//קבלת אחוזים כמה התחברו היום/ החודש/ השנה

endPointApi="https://localhost:44391/api/LoginHistory/";
//בנאי
constructor(private http:HttpClient) { }

//הוספת היסטוריה
async AddLoginHistory(Token:string):Promise<Observable<boolean>>
{
  return this.http.post<boolean>(this.endPointApi+'AddLoginHistory',{},{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//מחיקת היסטוריה לפי מזהה היסטוריה
async DeleteLoginHistoryById(Token:string,Id:number):Promise<Observable<any>>
{
  return this.http.delete<any>(this.endPointApi+'DeleteLoginHistoryById/'+Id,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
async GetLoginHistoryById(Token:string,Id:number):Promise<Observable<LoginHistory>>
{
  return this.http.get<LoginHistory>(this.endPointApi+'GetLoginHistoryById/'+Id,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
async GetAllLoginHistory(Token:string):Promise<Observable<Array<LoginHistory>>>
{
  return this.http.get<Array<LoginHistory>>(this.endPointApi+'GetAllLoginHistory',{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}


//קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
async GetAllLoginHistoryByUserId(Token:string,UserId:number):Promise<Observable<Array<LoginHistory>>>
{
  return this.http.get<Array<LoginHistory>>(this.endPointApi+'GetAllLoginHistoryByUserId/'+UserId,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
async GetLoginHistoryFilteringByDate(Token:string,RequestDate:string):Promise<Observable<Array<LoginHistory>>>
{
  return this.http.get<Array<LoginHistory>>(this.endPointApi+'GetLoginHistoryFilteringByDate/'+RequestDate,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//קבלת אחוזים כמה התחברו היום/ החודש/ השנה
async GetAllPrecentageLoginHistoryUserByDate(Token:string,RequestDate:string):Promise<Observable<number>>
{
  return this.http.get<number>(this.endPointApi+'GetAllPrecentageLoginHistoryUserByDate/'+RequestDate,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

}
