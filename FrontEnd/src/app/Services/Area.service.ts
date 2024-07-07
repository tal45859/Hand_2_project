import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Area } from '../Model/Area';
import { Token } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class AreaService {
//תקציר
//בנאי
//קבלת רשימת כל הערים
//קבלת עיר לפי מזהה
//קבלת רשימת ערים לפי מזהה אזור
//הוספת עיר מוגבל למנהל
//מחיקת עיר מוגבל למנהל
//עדכון עיר מוגבל למנהל

endPointApi="https://localhost:44391/api/Area/";
//בנאי
constructor(private http:HttpClient) { }

//קבלת רשימת כל הערים
async GetAllArea():Promise<Observable<Array<Area>>>
{
  return this.http.get<Array<Area>>(this.endPointApi+'GetAllArea');
}

//קבלת עיר לפי מזהה
async GetAreaById(Id:number):Promise<Observable<Area>>
{
  return this.http.get<Area>(this.endPointApi+'GetAreaById/'+Id);
}


//קבלת רשימת ערים לפי מזהה אזור
async GetAllAreaByTopAreaId(TopAreaId:number):Promise<Observable<Array<Area>>>
{
  return this.http.get<Array<Area>>(this.endPointApi+'GetAllAreaByTopAreaId/'+TopAreaId);
}

//הוספת עיר מוגבל למנהל
async AddArea(Token:string,AreaObjToAdd:Area):Promise<Observable<boolean>>
{
  return this.http.post<boolean>(this.endPointApi+'AddArea',AreaObjToAdd,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//מחיקת עיר מוגבל למנהל
async DeleteAreaById(Token:string,Id:number):Promise<Observable<any>>
{
  return this.http.delete<any>(this.endPointApi+'DeleteAreaById/'+Id,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

//עדכון עיר מוגבל למנהל
async UpdateArea(Token:string,AreObjToUpdate:Area):Promise<Observable<any>>
{
  return this.http.put<any>(this.endPointApi+'UpdateArea',AreObjToUpdate,{
    headers:new HttpHeaders().set('Authorization',""+Token)
  });
}

}
