import { Component, OnInit } from '@angular/core';
import { Favorite } from 'src/app/Model/Favorite';
import { Image } from 'src/app/Model/Image';
import { Post } from 'src/app/Model/Post';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { FavoriteService } from 'src/app/Services/Favorite.service';
import { ImageService } from 'src/app/Services/Image.service';
import { LoginService } from 'src/app/Services/Login.service';

@Component({
  selector: 'app-All-Favorite',
  templateUrl: './All-Favorite.component.html',
  styleUrls: ['./All-Favorite.component.css']
})
export class AllFavoriteComponent implements OnInit {

  public OpenSun=0;
  public Token:string="";
  public PostObjToSun?:Post={};
  public ArrAllFavorite:Array<Favorite>=[];
  public page=0;
  public ArrAllImages:Array<Image>=[];

  constructor(private HttpLogin:LoginService,private HttpFavorite:FavoriteService,private HttpImage:ImageService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.GetAllFavorite();
    this.GetAllImages();
  }

  async GetAllFavorite()
  {
    (await this.HttpFavorite.GetAllFavoriteByJWT(this.Token)).subscribe((response)=>{this.ArrAllFavorite=response})
  }

  async GetAllImages()
  {
    (await this.HttpImage.GetAllImage()).subscribe((response)=>{this.ArrAllImages=response});
  }

  public GetImageByPost(PostId?:number)
  {
    for(let i=0;i<this.ArrAllImages.length;i++)
    {
      if(this.ArrAllImages[i].PostId==PostId)
      {
        return this.ArrAllImages[i].Url;
      }
    }
    return "assets/defult.png";
  }

  async ClickDeleteFavorite(FavoriteId?:number)
  {
    (await this.HttpFavorite.DeleteFavoriteById(this.Token,Number(FavoriteId))).subscribe(
      (respons)=>{this.GetAllFavorite()},
      (error)=>{});
  }

  CloseTheSun()
  {
    this.OpenSun=0;
  }

}
