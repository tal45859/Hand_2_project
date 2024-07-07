import { Token } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Image } from '../Model/Image';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
//תקציר
//בנאי
//קבלת תמונה לפי מזהה
//קבלת רשימת כל התמונות
//הוספת תמונה ישירות לתיקיה
//קבלת רשימת תמונות לפי מזהה מודעה
//הוספת תמונה חדשה לבסיס נתונים
//מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
//מחיקת תמונה לוודא שזה מנהל
//בדיקת תקינות התמונה

endPointApi="https://localhost:44391/api/Image/";

//בנאי
constructor(private http:HttpClient) { }


//קבלת תמונה לפי מזהה
async GetImageById(Id:number):Promise<Observable<Image>>
{
  return this.http.get<Image>(this.endPointApi+'GetImageById/'+Id);
}

//קבלת רשימת כל התמונות
async GetAllImage():Promise<Observable<Array<Image>>>
{
  return this.http.get<Array<Image>>(this.endPointApi+'GetAllImage');
}

//הוספת תמונה ישירות לתיקיה
async AddImageToFolder(file:FormData, Token:string): Promise<Observable<string>>
{
  return this.http.post<string>(this.endPointApi+"AddImageToFolder",file,{
    headers: new HttpHeaders().set('Authorization', ""+Token)});
};


//קבלת רשימת תמונות לפי מזהה מודעה
async GetAllImageByPostId(PostId:number):Promise<Observable<Array<Image>>>
{
  return this.http.get<Array<Image>>(this.endPointApi+'GetAllImageByPostId/'+PostId);
}

//הוספת תמונה חדשה לבסיס נתונים
async AddImageToDB(Token:string,ImageObjToAdd:Image):Promise<Observable<boolean>>
{
  return this.http.post<boolean>(this.endPointApi+'AddImageToDB',ImageObjToAdd,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
async DeleteImageForUser(Token:string,ImageId:number):Promise<Observable<any>>
{
  return this.http.delete<any>(this.endPointApi+'DeleteImageForUser/'+ImageId,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//מחיקת תמונה לוודא שזה מנהל
async DeleteImageForAdmin(Token:string,ImageId:number):Promise<Observable<any>>
{
  return this.http.delete<any>(this.endPointApi+'DeleteImageForAdmin/'+ImageId,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}



}
