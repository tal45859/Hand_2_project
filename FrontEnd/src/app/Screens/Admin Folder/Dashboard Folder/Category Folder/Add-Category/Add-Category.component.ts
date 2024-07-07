import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/Model/Category';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { CategoryService } from 'src/app/Services/Category.service';
import { LoginService } from 'src/app/Services/Login.service';
import { SubAndCategoryValidationService } from 'src/app/Services/SubAndCategoryValidation.service';

@Component({
  selector: 'app-Add-Category',
  templateUrl: './Add-Category.component.html',
  styleUrls: ['./Add-Category.component.css']
})
export class AddCategoryComponent implements OnInit {
  public Token:string="";
  public ObjForAddCategory:Category={};
  public Spiner=false;
  public IsCreate=false;
  public ResponseMessage:ResponseValidation={Isok:true,Message:''};
  

  constructor(private httpLogin:LoginService,private HttpCategory:CategoryService,private Validation:SubAndCategoryValidationService) { }

  ngOnInit() {
    this.Token=this.httpLogin.Token;
  }

  async ClickAddCategory()
  {
    this.Spiner=true;
    this.ResponseMessage=this.Validation.CheckCategoryAndSubName(this.ObjForAddCategory.CategoryName);
    if(!this.ResponseMessage.Isok)
    {
      this.Spiner=false;
      this.ResponseMessage.Message+='קטגוריה!'
      return;
    }
     (await this.HttpCategory.AddCategory(this.Token,this.ObjForAddCategory)).subscribe((response)=>{this.Spiner=false,this.IsCreate=true,this.ObjForAddCategory.CategoryName='',this.AfterJoin()});
  }

  public AfterJoin()
  {
    setTimeout(() => {
      this.IsCreate=false;
    }, 1500);
  }



}
