import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/Model/Category';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { Subcategory } from 'src/app/Model/Subcategory';
import { CategoryService } from 'src/app/Services/Category.service';
import { LoginService } from 'src/app/Services/Login.service';
import { SubAndCategoryValidationService } from 'src/app/Services/SubAndCategoryValidation.service';
import { SubcategoryService } from 'src/app/Services/Subcategory.service';

@Component({
  selector: 'app-Add-SubCategory',
  templateUrl: './Add-SubCategory.component.html',
  styleUrls: ['./Add-SubCategory.component.css']
})
export class AddSubCategoryComponent implements OnInit {
  public Token:string="";
  public ObjForAddSubCategory:Subcategory={};
  public ArrCategory:Array<Category>=[];
  public Spiner=false;
  public IsCreate=false;
  public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:'אנא בחק קטגוריה ראשית'}];
;
  constructor(private HttpLogin:LoginService,private HttpSubCategory:SubcategoryService,private HttpCategory:CategoryService,private Validation:SubAndCategoryValidationService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.GetALlCategory();
  }

  async GetALlCategory()
  {
    (await this.HttpCategory.GetAllCategory()).subscribe((response)=>{this.ArrCategory=response});
  }

  async ClickAddSubCategory()
  {
    this.ResponseMessage[0]=this.Validation.CheckCategoryAndSubName(this.ObjForAddSubCategory.SubcategoryName);
    this.ResponseMessage[1].Isok=this.ObjForAddSubCategory.CategoryId!=undefined&&this.ObjForAddSubCategory.CategoryId!=0;
    if(!this.ResponseMessage[0].Isok||!this.ResponseMessage[1].Isok)
    {
      this.ResponseMessage[0].Message+='תת קטגוריה!'
      this.Spiner=true;
      return;
    }
    (await this.HttpSubCategory.AddSubcategory(this.Token,this.ObjForAddSubCategory)).subscribe((response)=>{this.Spiner=false,this.IsCreate=true,this.ObjForAddSubCategory.SubcategoryName='',this.ObjForAddSubCategory.CategoryId=undefined,this.AfterJoin()});
  }

  public AfterJoin()
  {
    setTimeout(() => {
      this.IsCreate=false;
    }, 1500);
  }



}






