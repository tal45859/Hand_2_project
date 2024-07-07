import { Reporting } from './../Model/Reporting';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, identity } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportingService {

  //תקציר
  //בנאי
  //קבלת דיווח לפי מזהה
  //הוספת דיווח
  //מחיקת דיווח
  //שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
  //קבלת רשימת דיווחים לפי מזהה מודעה
  //קבלת רשימת כל הדיווחים
  //קבלת רשימת הדיווחים הלא פתורים
  //קבלת רשימת הדיווחים הפתורים

  endPointApi="https://localhost:44391/api/Reporting/";
 //בנאי
constructor(private http:HttpClient) { }


  //קבלת דיווח לפי מזהה
  async GetReportingById(Token:string,Id:number):Promise<Observable<Reporting>>
  {
    return this.http.get<Reporting>(this.endPointApi+'GetReportingById/'+Id,{
      headers:new HttpHeaders().set('Authorization',""+Token)
    });
  }

  //הוספת דיווח
  async AddReporting(ReportingObj:Reporting):Promise<Observable<boolean>>
  {
    return this.http.post<boolean>(this.endPointApi+'AddReporting',ReportingObj);
  }

  //מחיקת דיווח
  async DeleteReportingById(Token:string,Id:number):Promise<Observable<any>>
  {
    return this.http.delete<any>(this.endPointApi+"DeleteReportingById/"+Id,{
      headers:new HttpHeaders().set('Authorization',""+Token)
    });
  }

  //שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
  async UpdateReporting(Token:string,ReportingToUpdate:Reporting):Promise<Observable<any>>
  {
    return this.http.put<any>(this.endPointApi+"UpdateReporting",ReportingToUpdate,{
      headers:new HttpHeaders().set('Authorization',""+Token)
    });
  }

  //קבלת רשימת דיווחים לפי מזהה מודעה
  async GetAllReportingByPostId(Token:string,Id:number):Promise<Observable<Array<Reporting>>>
  {
    return this.http.get<Array<Reporting>>(this.endPointApi+'GetAllReportingByPostId/'+Id,{
      headers:new HttpHeaders().set('Authorization',""+Token)
    });
  }

  //קבלת רשימת כל הדיווחים
  async GetAllReporting(Token:string):Promise<Observable<Array<Reporting>>>
  {
    return this.http.get<Array<Reporting>>(this.endPointApi+"GetAllReporting",{
      headers:new HttpHeaders().set('Authorization',""+Token)
    });
  }

  //קבלת רשימת הדיווחים הלא פתורים
  async GetAllReportingActive(Token:string):Promise<Observable<Array<Reporting>>>
  {
    return this.http.get<Array<Reporting>>(this.endPointApi+"GetAllReportingActive",{
      headers:new HttpHeaders().set('Authorization',""+Token)
    });
  }

  //קבלת רשימת הדיווחים הפתורים
  async GetAllReportingNoActive(Token:string):Promise<Observable<Array<Reporting>>>
  {
    return this.http.get<Array<Reporting>>(this.endPointApi+"GetAllReportingNoActive",{
      headers:new HttpHeaders().set('Authorization',""+Token)
    });
  }

}
