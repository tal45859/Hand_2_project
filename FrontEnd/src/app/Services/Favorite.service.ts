import { Observable } from 'rxjs';
import { Favorite } from './../Model/Favorite';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {
//תקציר
//בנאי
//הוספת מועדף
//מחיקת מועדף
//JWT קבלת מועדף לפי מזהה מועדף ולפי
//JWT קבלת רשימת מועדפים לפי

endPointApi="https://localhost:44391/api/Favorite/";
//בנאי
constructor(private http:HttpClient) { }

//הוספת מועדף
async AddFavorite(Token:string,FavoriteObjToAdd:Favorite):Promise<Observable<boolean>>
{
  return this.http.post<boolean>(this.endPointApi+"AddFavorite",FavoriteObjToAdd,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//מחיקת מועדף
async DeleteFavoriteById(Token:string,FavoriteId:number):Promise<Observable<any>>
{
  return this.http.delete<any>(this.endPointApi+'DeleteFavoriteById/'+FavoriteId,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//JWT קבלת מועדף לפי מזהה מועדף ולפי
async GetFavoriteByFavoriteIdAndJWT(Token:string,FavoriteId:number):Promise<Observable<Favorite>>
{
  return this.http.get<Favorite>(this.endPointApi+'GetFavoriteByFavoriteIdAndJWT/'+FavoriteId,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//JWT קבלת רשימת מועדפים לפי
async GetAllFavoriteByJWT(Token:string):Promise<Observable<Array<Favorite>>>
{
  return this.http.get<Array<Favorite>>(this.endPointApi+'GetAllFavoriteByJWT',{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

}
