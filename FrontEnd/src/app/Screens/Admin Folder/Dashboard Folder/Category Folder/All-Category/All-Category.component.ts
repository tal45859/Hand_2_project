import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/Model/Category';
import { Subcategory } from 'src/app/Model/Subcategory';
import { CategoryService } from 'src/app/Services/Category.service';
import { LoginService } from 'src/app/Services/Login.service';
import { SubAndCategoryValidationService } from 'src/app/Services/SubAndCategoryValidation.service';
import { SubcategoryService } from 'src/app/Services/Subcategory.service';

@Component({
  selector: 'app-All-Category',
  templateUrl: './All-Category.component.html',
  styleUrls: ['./All-Category.component.css']
})
export class AllCategoryComponent implements OnInit {

  public SubCategoryObjForUpdate:Subcategory={};
  public CategoryObjForUpdate:Category={};
  public ArrCategory:Array<Category>=[];
  public ArrSubCategory:Array<Subcategory>=[];
  public Token:string="";
  public SubCategoryId?:number;
  public CategoryId?:number;
  public CategoryIdForGetAllSubCategory?:number;
  public page:number=0;
  public page1:number=0;
  public DesBefElmentID?:number=0;
  public ResponseMessage:ResponseValidation={Isok:true,Message:''}
  public DeleteOrUpdate:Array<boolean>=[true,true];//מקום 0 אם זה אם זה מחיקה או עדכון
                                                   //מקום 1 זה אם זה קטגוריה או תת קטגוריה
  constructor(private HttpCategory:CategoryService,private HttpSubCategory:SubcategoryService,private HttpLogin:LoginService,private SubAndCategoryValidation:SubAndCategoryValidationService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.GetAllCategory();
  }

  async GetAllCategory()
  {
    (await this.HttpCategory.GetAllCategory()).subscribe(
      (response)=>{this.ArrCategory=response});
  }

  async GetAllSubCategoryByCAtegoryId()
  {
    this.page1=0;
    (await this.HttpSubCategory.GetAllSubcategoryByCategoryId(Number(this.CategoryIdForGetAllSubCategory))).subscribe(
      (response)=>{this.ArrSubCategory=response});
  }

  public ClickOpenSub(ItemId?:number)
  {
    const ClassPanel= document?.getElementById(""+ItemId)?.classList;
    if(ClassPanel?.toString() == "removeDisplay")//אם הוא שווה לו תפתח
    {
      document?.getElementById(""+ItemId)?.classList.remove('removeDisplay');
      document?.getElementById(""+ItemId)?.classList.add('AddDisplay');
      //להוריד את המחק והערוך לקטגוריה
      document?.getElementById("Category"+ItemId)?.classList.add('removeDisplay');

    }
    else //אם הוא שונה תסגור
    {
      document?.getElementById(""+ItemId)?.classList.remove('AddDisplay');
      document?.getElementById(""+ItemId)?.classList.add('removeDisplay');
      //להוסיף את המחק והערוך לקטגוריה
      document?.getElementById("Category"+ItemId)?.classList.remove('removeDisplay');
    }

      // //למחוק לפאנל הקודם את האפשרות להציג
      if(ItemId!=this.DesBefElmentID)
      {
        document?.getElementById(""+this.DesBefElmentID)?.classList.remove('AddDisplay');
        document?.getElementById(""+this.DesBefElmentID)?.classList.add('removeDisplay');
        document?.getElementById("Category"+this.DesBefElmentID)?.classList.remove('removeDisplay');
        this.DesBefElmentID=ItemId;
      }

  }

  async ClickDeleteCategory()
  {
    (await this.HttpCategory.DeleteCategoryById(this.Token,Number(this.CategoryId))).subscribe(
      (response)=>{this.GetAllCategory()},
      (error)=>{}
    );
  }

  async ClickDeleteSubCategory()
  {
    (await this.HttpSubCategory.DeleteSubcategoryById(this.Token,Number(this.SubCategoryId))).subscribe(
      (response)=>{this.GetAllSubCategoryByCAtegoryId();},
      (error)=>{}
    );
  }

  async ClickUpdateCategory()
  {
    this.ResponseMessage.Isok=true;
    this.ResponseMessage=this.SubAndCategoryValidation.CheckCategoryAndSubName(this.CategoryObjForUpdate.CategoryName);
    if(!this.ResponseMessage.Isok)
    {
      return;
    }
    (await this.HttpCategory.UpdateCategory(this.Token,this.CategoryObjForUpdate)).subscribe(
      (response)=>{this.GetAllCategory()},
      (error)=>{}
    );
  }

  async ClickUpdateSubCategory()
  {
    this.ResponseMessage.Isok=true;
    this.ResponseMessage=this.SubAndCategoryValidation.CheckCategoryAndSubName(this.SubCategoryObjForUpdate.SubcategoryName);
    if(!this.ResponseMessage.Isok)
    {
      return;
    }
    (await this.HttpSubCategory.UpdateSubcategory(this.Token,this.SubCategoryObjForUpdate)).subscribe(
      (response)=>{this.GetAllSubCategoryByCAtegoryId()},
      (error)=>{}
    );
  }


}
