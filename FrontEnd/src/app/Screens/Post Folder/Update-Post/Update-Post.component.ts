import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Area } from 'src/app/Model/Area';
import { Category } from 'src/app/Model/Category';

import { Post } from 'src/app/Model/Post';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { Subcategory } from 'src/app/Model/Subcategory';
import { AreaService } from 'src/app/Services/Area.service';
import { CategoryService } from 'src/app/Services/Category.service';
import { LoginService } from 'src/app/Services/Login.service';
import { PostService } from 'src/app/Services/Post.service';
import { PostValidationService } from 'src/app/Services/PostValidation.service';
import { SubcategoryService } from 'src/app/Services/Subcategory.service';

@Component({
  selector: 'app-Update-Post',
  templateUrl: './Update-Post.component.html',
  styleUrls: ['./Update-Post.component.css']
})
export class UpdatePostComponent implements OnInit {

  @Output() CloseMeOutpot = new EventEmitter<number>();
  @Input()PostId?:number;
  public PostObjForUpdate:Post={};
  public Token:string="";
  public ArrAllCategory:Array<Category>=[];
  public ArrAllSubCategory:Array<Subcategory>=[];
  public ArrAllArea:Array<Area>=[];
  public CategoryId?:number;
  public spiner=false;
  public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''}
                                                  ,{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''}];

  constructor(private HttpLogin:LoginService, private HttpPost:PostService,private ValidPost:PostValidationService, private HttpCategory:CategoryService
    ,private HttpSubCategory:SubcategoryService,private HttpArea:AreaService) { }

  ngOnInit() {
    this.Token = this.HttpLogin.Token;
    this.GetPostById();
    this.GetAllCategory();
    this.GetAllArea();


  }

  async ClickUpdatePost()
  {
    this.spiner = true;
    this.ResponseMessage[0] = this.ValidPost.CheckTitle(this.PostObjForUpdate?.Title);
    this.ResponseMessage[1] = this.ValidPost.CheckBody(this.PostObjForUpdate?.Body);
    this.ResponseMessage[2] = this.ValidPost.CheckPrice(this.PostObjForUpdate?.Price);
    this.ResponseMessage[3] = this.ValidPost.CheckCategoryId(this.CategoryId);
    this.ResponseMessage[4] = this.ValidPost.CheckSubCategoryId(this.PostObjForUpdate?.SubcategoryId);
    this.ResponseMessage[5] = this.ValidPost.CheckAreaId(this.PostObjForUpdate?.AreaId);
    if(!this.ResponseMessage[0].Isok||!this.ResponseMessage[1].Isok||!this.ResponseMessage[2].Isok||
      !this.ResponseMessage[3].Isok||!this.ResponseMessage[4].Isok||!this.ResponseMessage[5].Isok)
      {
        this.spiner = false;
        return;
      }
      this.spiner = false;
      this.PostObjForUpdate.User=undefined;
      this.PostObjForUpdate.Area=undefined;
      this.PostObjForUpdate.Subcategory=undefined;
    (await this.HttpPost.UpdatePost(this.Token,this.PostObjForUpdate)).subscribe((response)=>{this.GoNext()},(error)=>{this.GoNext()});
  }

  async GetPostById()
  {
    (await this.HttpPost.GetPostById(Number(this.PostId))).subscribe((response)=>{this.PostObjForUpdate=response,
      this.CategoryId=response.Subcategory?.CategoryId,
      this.GetALlSubCategoryByCategoryId()});
  }

  async GetAllCategory()
  {
    (await this.HttpCategory.GetAllCategory()).subscribe((response)=>{this.ArrAllCategory=response});
  }

  async GetALlSubCategoryByCategoryId()
  {

    (await this.HttpSubCategory.GetAllSubcategoryByCategoryId(Number(this.CategoryId))).subscribe((response)=>{this.ArrAllSubCategory=response});
  }

  async GetAllArea()
  {
    (await this.HttpArea.GetAllArea()).subscribe((response)=>{this.ArrAllArea=response});
  }

  async GoNext()
  {
    this.CloseMeOutpot.emit(1);
  }

}
