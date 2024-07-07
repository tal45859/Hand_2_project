import { Subcategory } from './../Model/Subcategory';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SubcategoryService {

//בנאי
//קבלת תת קטגוריה לפי מזהה תת קטגוריה
//קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
//קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
//קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
// הוספת תת קטגוריה
// עדכון תת קטגוריה
// מחיקת תת קטגוריה

endPointApi="https://localhost:44391/api/Subcategory/";
//בנאי
constructor(private http:HttpClient) { }

//קבלת תת קטגוריה לפי מזהה תת קטגוריה
async GetSubcategoryById(SubcategoryId:number):Promise<Observable<Subcategory>>
{
  return this.http.get<Subcategory>(this.endPointApi+"GetSubcategoryById/"+SubcategoryId);
}

//קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
async GetAllSubcategory():Promise<Observable<Array<Subcategory>>>
{
  return this.http.get<Array<Subcategory>>(this.endPointApi+"GetAllSubcategory");
}

//קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
async GetSubcategoryByCategoryId(CategoryId:number):Promise<Observable<Subcategory>>
{
  return this.http.get<Subcategory>(this.endPointApi+"GetSubcategoryByCategoryId/"+CategoryId);
}

//קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
async GetAllSubcategoryByCategoryId(CategoryId:number):Promise<Observable<Array<Subcategory>>>
{
  return this.http.get<Array<Subcategory>>(this.endPointApi+"GetAllSubcategoryByCategoryId/"+CategoryId);
}

// הוספת תת קטגוריה
async AddSubcategory(Token:string,SubCategoryObjToAdd:Subcategory):Promise<Observable<boolean>>
{
  return this.http.post<boolean>(this.endPointApi+"AddSubcategory",SubCategoryObjToAdd,{
    headers: new HttpHeaders().set('Authorization',""+Token)
  });
}

// עדכון תת קטגוריה
async UpdateSubcategory(Token:string,subcategoryToUpdate:Subcategory):Promise<Observable<any>>
{
  return this.http.put<any>(this.endPointApi+"UpdateSubcategory",subcategoryToUpdate,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

// מחיקת תת קטגוריה
async DeleteSubcategoryById(Token:string,SubcategoryId:number):Promise<Observable<any>>
{
  return this.http.delete<any>(this.endPointApi+"DeleteSubcategoryById/"+SubcategoryId,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

}
