import { Injectable } from '@angular/core';
import { Post } from '../Model/Post';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PostService {
//תקציר
//בנאי
//JWT הוספת מודעה על פי
//הוספת צפיה למודעה לפי מזהה מודעה
//מחיקת מודעה למשתמש ומנהל
//עדכון מודעה למי שייצר אותו
//JWT קבלת מזהה מודעה אחרון על פי
//קבלת מודעה לפי מזהה מודעה עם אויבקט
//קבלת רשימת כל המודעות עם אוביקטים
// JWT קבלת רשימת כל המודעות של משתמש למשתמש לפי
//קבלת רשימת כל המודעות של משתמש מסוים לפי מזהה משתמש
//קבלת רשימת כל המודעות לפי מזהה תת קטגוריה
//קבלת רשימת כל המודעות לפי מזהה קטגוריה
//קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר
//JWT קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר של משתמש על פי
//קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר
//JWT קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר של משתמש על פי
//קבלת אחוזים כמה מודעות התוספו היום/ החודש/ השנה

endPointApi="https://localhost:44391/api/Post/";
  //בנאי
constructor(private http:HttpClient) { }

//JWT הוספת מודעה על פי
async AddPost(Token:string,PostObjToAdd:Post):Promise<Observable<boolean>>
{
  return this.http.post<boolean>(this.endPointApi+"AddPost",PostObjToAdd,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//הוספת צפיה למודעה לפי מזהה מודעה
async AddViewsToPost(Id:number):Promise<Observable<any>>
{
  return this.http.get<any>(this.endPointApi+"AddViewsToPost/"+Id);
}

//מחיקת מודעה למשתמש ומנהל
async DeletePostById(Token:string,Id:number):Promise<Observable<any>>
{
  return this.http.delete<any>(this.endPointApi+'DeletePostById/'+Id,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//עדכון מודעה למי שייצר אותו
async UpdatePost(Token:string,PostToUpdatObj:Post):Promise<Observable<any>>
{
  return this.http.put<any>(this.endPointApi+"UpdatePost",PostToUpdatObj,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//JWT קבלת מזהה מודעה אחרון על פי
async GetLastPostIdByJWTForUser(Token:string):Promise<Observable<number>>
{
  return this.http.get<number>(this.endPointApi+'GetLastPostIdByJWTForUser',{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//קבלת מודעה לפי מזהה מודעה עם אויבקט
async GetPostById(Id:number):Promise<Observable<Post>>
{
  return this.http.get<Post>(this.endPointApi+"GetPostById/"+Id);
}

//קבלת רשימת כל המודעות עם אוביקטים
async GetAllPost():Promise<Observable<Array<Post>>>
{
  return this.http.get<Array<Post>>(this.endPointApi+'GetAllPost');
}

// JWT קבלת רשימת כל המודעות של משתמש למשתמש לפי
async GetAllPostByJWT(Token:string):Promise<Observable<Array<Post>>>
{
  return this.http.get<Array<Post>>(this.endPointApi+"GetAllPostByJWT",{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//קבלת רשימת כל המודעות של משתמש מסוים לפי מזהה משתמש
async GetAllPostByUserId(UserId:number):Promise<Observable<Array<Post>>>
{
  return this.http.get<Array<Post>>(this.endPointApi+"GetAllPostByUserId/"+UserId);
}

//קבלת רשימת כל המודעות לפי מזהה תת קטגוריה
async GetAllPostBySubcategoryId(SubcategoryId:number):Promise<Observable<Array<Post>>>
{
  return this.http.get<Array<Post>>(this.endPointApi+"GetAllPostBySubcategoryId/"+SubcategoryId);
}

//קבלת רשימת כל המודעות לפי מזהה קטגוריה
async GetAllPostByCategoryId(CategoryId:number):Promise<Observable<Array<Post>>>
{
  return this.http.get<Array<Post>>(this.endPointApi+"GetAllPostByCategoryId/"+CategoryId);
}

//קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר
async GetAllPostByNumberOfViews():Promise<Observable<Array<Post>>>
{
  return this.http.get<Array<Post>>(this.endPointApi+"GetAllPostByNumberOfViews");
}

//JWT קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר של משתמש על פי
async GetAllPostByNumberOfViewsByJWTForUser(Token:string):Promise<Observable<Array<Post>>>
{
  return this.http.get<Array<Post>>(this.endPointApi+'GetAllPostByNumberOfViewsByJWTForUser',{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר
async GetAllPostByUploadDate():Promise<Observable<Array<Post>>>
{
  return this.http.get<Array<Post>>(this.endPointApi+"GetAllPostByUploadDate");
}

//JWT קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר של משתמש על פי
async GetAllPostByUploadDateByJWTForUser(Token:string):Promise<Observable<Array<Post>>>
{
  return this.http.get<Array<Post>>(this.endPointApi+'GetAllPostByUploadDateByJWTForUser',{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//קבלת אחוזים כמה מודעות התוספו היום/ החודש/ השנה
async GetAllPrecentagePostUploadByDate(Token:string,RequestDate:string):Promise<Observable<number>>
{
  return this.http.get<number>(this.endPointApi+'GetAllPrecentagePostUploadByDate/'+RequestDate,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

}
