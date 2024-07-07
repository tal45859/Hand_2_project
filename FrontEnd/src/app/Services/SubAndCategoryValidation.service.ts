import { Injectable } from '@angular/core';
import { ResponseValidation } from '../Model/ResponseValidation';

@Injectable({
  providedIn: 'root'
})
export class SubAndCategoryValidationService {

constructor() { }

CheckCategoryAndSubName(SubAndCategoryName?:string):ResponseValidation
 {
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(SubAndCategoryName==null || SubAndCategoryName.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן שם ";
   return Response;
  }
  //בדיקה אם קיים במחרוזת תווים אסורים
   else if(/[^a-zA-Zא-ת ]/.test(SubAndCategoryName))
   {
     Response.Isok=false;
     Response.Message="אנא הזן אותיות באנגלית או בעברית בלבד!";
     return Response;
   }
   Response.Isok=true
   return  Response;
 }


}
