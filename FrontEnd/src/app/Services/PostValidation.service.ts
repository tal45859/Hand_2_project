import { Injectable } from '@angular/core';
import { ResponseValidation } from '../Model/ResponseValidation';

@Injectable({
  providedIn: 'root'
})
export class PostValidationService {

constructor() { }


public CheckCategoryId(CategoryId?:number):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(CategoryId==null||CategoryId==undefined || CategoryId==0)
  {
   Response.Isok=false;
   Response.Message="אנא בחר קטגוריה!";
   return Response;
  }
   Response.Isok=true
   return  Response;
}

public CheckSubCategoryId(SubCategoryId?:number):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(SubCategoryId==null||SubCategoryId==undefined || SubCategoryId==0)
  {
   Response.Isok=false;
   Response.Message="אנא בחר תת קטגוריה!";
   return Response;
  }
   Response.Isok=true
   return  Response;
}

public CheckAreaId(AreaId?:number):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(AreaId==null||AreaId==undefined || AreaId==0)
  {
   Response.Isok=false;
   Response.Message="אנא בחר אזור!";
   return Response;
  }
   Response.Isok=true
   return  Response;
}

public CheckTitle(Title?:string):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(Title==null || Title.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן כותרת!";
   return Response;
  }
  //בדיקה אם קיים במחרוזת תווים אסורים
   else if(/[^a-zA-Zא-ת ]/.test(Title))
   {
     Response.Isok=false;
     Response.Message="אנא הזן אותיות באנגלית או בעברית בלבד!";
     return Response;
   }
   Response.Isok=true
   return  Response;
}

public CheckBody(Body?:string):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(Body==null || Body.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן תוכן!";
   return Response;
  }
  Response.Isok=true
  return  Response;
}

public CheckPrice(Price?:string):ResponseValidation
{
  let Response:ResponseValidation={};
  if(Price==null || Price.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן מחיר!";
   return Response;
  }
  else if(/[^0-9 ]/.test(Price))
  {
    Response.Isok=false;
    Response.Message="אנא הזן מחיר במספר שלם בלבד!";
    return Response;
  }
  Response.Isok=true
  return  Response;
}


}
