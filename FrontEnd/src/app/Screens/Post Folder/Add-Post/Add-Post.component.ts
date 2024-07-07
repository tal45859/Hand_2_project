import { Subcategory } from 'src/app/Model/Subcategory';
import { Category } from './../../../Model/Category';
import { Component, EventEmitter, OnInit, Output, OnChanges, SimpleChanges } from '@angular/core';
import { CategoryService } from 'src/app/Services/Category.service';
import { LoginService } from 'src/app/Services/Login.service';
import { PostService } from 'src/app/Services/Post.service';
import { Post } from 'src/app/Model/Post';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { Area } from 'src/app/Model/Area';
import { AreaService } from 'src/app/Services/Area.service';
import { SubcategoryService } from 'src/app/Services/Subcategory.service';
import { PostValidationService } from 'src/app/Services/PostValidation.service';

@Component({
  selector: 'app-Add-Post',
  templateUrl: './Add-Post.component.html',
  styleUrls: ['./Add-Post.component.css']
})
export class AddPostComponent implements OnInit  {

  @Output() CloseMeOutpot = new EventEmitter<number>();
  public Token:string="";
  public ArrAllCategory:Array<Category>=[];
  public ArrAllSubCategory:Array<Subcategory>=[];
  public ArrAllArea:Array<Area>=[];
  public CategoryId?:number;
  public PostObjToAdd:Post={};
  public spiner=false;
  public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''}
                                                  ,{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''}];

  constructor(private HttpLogin:LoginService, private HttpPost:PostService,private ValidPost:PostValidationService, private HttpCategory:CategoryService,private HttpSubCategory:SubcategoryService,private HttpArea:AreaService) { }

  ngOnInit() {
    this.Token = this.HttpLogin.Token;
    this.GetAllCategory();
    this.GetAllArea();
  }

  async ClickAddPost()
  {
    this.spiner = true;
    this.ResponseMessage[0] = this.ValidPost.CheckTitle(this.PostObjToAdd.Title);
    this.ResponseMessage[1] = this.ValidPost.CheckBody(this.PostObjToAdd.Body);
    this.ResponseMessage[2] = this.ValidPost.CheckPrice(this.PostObjToAdd.Price);
    this.ResponseMessage[3] = this.ValidPost.CheckCategoryId(this.CategoryId);
    this.ResponseMessage[4] = this.ValidPost.CheckSubCategoryId(this.PostObjToAdd.SubcategoryId);
    this.ResponseMessage[5] = this.ValidPost.CheckAreaId(this.PostObjToAdd.AreaId);
    if(!this.ResponseMessage[0].Isok||!this.ResponseMessage[1].Isok||!this.ResponseMessage[2].Isok||
      !this.ResponseMessage[3].Isok||!this.ResponseMessage[4].Isok||!this.ResponseMessage[5].Isok)
      {
        this.spiner = false;
        return;
      }
      this.spiner = false;
    (await this.HttpPost.AddPost(this.Token,this.PostObjToAdd)).subscribe((response)=>{this.GetLastPost()},(error)=>{});
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

  async GetLastPost()
  {
    (await this.HttpPost.GetLastPostIdByJWTForUser(this.HttpLogin.Token)).subscribe(
      (response)=>{this.CloseMeOutpot.emit(response)}
    );
  }



}
