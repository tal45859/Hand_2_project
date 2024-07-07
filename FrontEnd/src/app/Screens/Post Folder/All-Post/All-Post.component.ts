import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Favorite } from 'src/app/Model/Favorite';
import { Image } from 'src/app/Model/Image';
import { Post } from 'src/app/Model/Post';
import { Reporting } from 'src/app/Model/Reporting';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { Subcategory } from 'src/app/Model/Subcategory';
import { FavoriteService } from 'src/app/Services/Favorite.service';
import { ImageService } from 'src/app/Services/Image.service';
import { LoginService } from 'src/app/Services/Login.service';
import { PostService } from 'src/app/Services/Post.service';
import { ReportingService } from 'src/app/Services/Reporting.service';
import { SubcategoryService } from 'src/app/Services/Subcategory.service';

@Component({
  selector: 'app-All-Post',
  templateUrl: './All-Post.component.html',
  styleUrls: ['./All-Post.component.css']
})
export class AllPostComponent implements OnInit ,OnChanges{
  public Token:string="";
  public Role:string="";
  public page=1;
  public OldCategoryId?:number=0;
  public ArrAllPost:Array<Post>=[];
  public ArrAllSubCategory:Array<Subcategory>=[];
  public AllImageForPost:Array<Image>=[];
  public SubCategoryId?:number=0;
  @Input() CategoryId?:number;
  public PostObjToSun:Post={};
  public OpenSun=0;
  public ReportingObj:Reporting={};
  public ResponseMessage:ResponseValidation={Isok:true,Message:'אנא כתוב סיבת דיווח!'};

  constructor(private HttpLogin:LoginService ,private HttpPost:PostService, private HttpImage:ImageService
    ,private HttpSubCategory:SubcategoryService,private HttpFavorite:FavoriteService,private HttpReporting:ReportingService) { }


  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.Role=this.HttpLogin.Role;
    if(this.CategoryId==0)
      {
        this.CategoryId=1;
      }
    this.OldCategoryId=this.CategoryId;
    this.GetAllPostByCategoryId();
    this.GetAllSubCategoryByCategoryId();
    this.GetAllImage();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.CategoryId!=this.OldCategoryId)
    {
      this.GetAllPostByCategoryId()
      this.GetAllSubCategoryByCategoryId();
    }
   }

  async GetAllPostByCategoryId()
  {
    (await this.HttpPost.GetAllPostByCategoryId(Number(this.CategoryId))).subscribe(
      (response)=>{this.ArrAllPost=response}
    );
  }

  //לבדוק ברגע שיהיו פוסטים רצינים ולוודא שהוא מסנן לפי תת קטגוריה
   async SortBySubCategory()
  {
   (await this.HttpPost.GetAllPostBySubcategoryId(Number(this.SubCategoryId))).subscribe((response)=>{this.ArrAllPost=response});
  }

  async GetAllSubCategoryByCategoryId()
  {
    (await this.HttpSubCategory.GetAllSubcategoryByCategoryId(Number(this.CategoryId))).subscribe((response)=>{this.ArrAllSubCategory=response});
  }

  async GetAllImage()
  {
    (await this.HttpImage.GetAllImage()).subscribe((response)=>{this.AllImageForPost=response});
  }


  public GetImageByPost(PostId?:number)
  {
    for(let i=0;i<this.AllImageForPost.length;i++)
    {
      if(this.AllImageForPost[i].PostId==PostId)
      {
        return this.AllImageForPost[i].Url;
      }
    }
    return "assets/defult.png";
  }


  async ClickAddFavorite(PostId?:number)
  {
    let FavoriteObj:Favorite={'PostId':PostId};
    (await this.HttpFavorite.AddFavorite(this.Token,FavoriteObj)).subscribe(
      (response)=>{},
      (error)=>{}
    );
    //להוסיף משהו שהצליח
  }

  async ClickDeleteForAdmin(PostId?:number)
  {
    (await this.HttpPost.DeletePostById(this.Token,Number(PostId))).subscribe(
      (response)=>{this.GetAllPostByCategoryId();},
      (error)=>{}
    );
       //להוסיף משהו שהצליח
  }

  async AddReporting()
  {

    if(this.ReportingObj.Cause==undefined||this.ReportingObj.Cause==null||this.ReportingObj.Cause=='')
      {
        this.ResponseMessage.Isok=false;
        return;
      }
    (await this.HttpReporting.AddReporting(this.ReportingObj)).subscribe(
      (response)=>{},
      (error)=>{}
    );
      //להוסיף משהו שהצליח
  }

  CloseTheSun()
  {
    this.OpenSun=0;
  }

}
