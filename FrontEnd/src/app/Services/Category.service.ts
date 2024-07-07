import { Category } from './../Model/Category';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
//תקציר
// בנאי
// קבלת קטגוריה לפי מזהה
// קבלת קטגוריה לפי שם קטגוריה
// קבלת כל הקטגוריות
// יצירת קטגוריה
// מחיקת קטגוריה
// עדכון קטגוריה
endPointApi="https://localhost:44391/api/Category/";

// בנאי
constructor(private http:HttpClient) { }

// קבלת קטגוריה לפי מזהה
async GetCategoryById(Id:number):Promise<Observable<Category>>
{
  return this.http.get<Category>(this.endPointApi+'GetCategoryById/'+Id);
}

// קבלת קטגוריה לפי שם קטגוריה
async GetAreaByName(Name:string):Promise<Observable<Category>>
{
  return this.http.get<Category>(this.endPointApi+'GetAreaByName/'+Name);
}

// קבלת כל הקטגוריות
async GetAllCategory():Promise<Observable<Array<Category>>>
{
  return this.http.get<Array<Category>>(this.endPointApi+'GetAllCategory');
}

// יצירת קטגוריה
async AddCategory(Token:string,CategoryObjToAdd:Category):Promise<Observable<boolean>>
{
  return this.http.post<boolean>(this.endPointApi+'AddCategory',CategoryObjToAdd,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

// מחיקת קטגוריה
async DeleteCategoryById(Token:string,Id:number):Promise<Observable<any>>
{
  return this.http.delete<any>(this.endPointApi+'DeleteCategoryById/'+Id,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

// עדכון קטגוריה
async UpdateCategory(Token:string,CategoryToUpdateObj:Category):Promise<Observable<any>>
{
  return this.http.put<any>(this.endPointApi+'UpdateCategory',CategoryToUpdateObj,{
    headers: new HttpHeaders().set('Authorization',""+Token)
  });
}

}
