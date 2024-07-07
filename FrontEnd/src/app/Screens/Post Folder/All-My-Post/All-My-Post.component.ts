import { Component, OnInit } from '@angular/core';
import { Image } from 'src/app/Model/Image';
import { Post } from 'src/app/Model/Post';
import { ImageService } from 'src/app/Services/Image.service';
import { LoginService } from 'src/app/Services/Login.service';
import { PostService } from 'src/app/Services/Post.service';

@Component({
  selector: 'app-All-My-Post',
  templateUrl: './All-My-Post.component.html',
  styleUrls: ['./All-My-Post.component.css']
})
export class AllMyPostComponent implements OnInit {

  public Token:string="";
  public Role:string="";
  public page=1;
  public ArrAllPost:Array<Post>=[];
  public AllImageForPost:Array<Image>=[];
  public PostObjToSun:Post={};
  public OpenSun=0;

  constructor(private HttpLogin:LoginService ,private HttpPost:PostService, private HttpImage:ImageService
    ) { }


  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.GetAllPostByJWT();
    this.GetAllImage();
  }



  async GetAllPostByJWT()
  {
    (await this.HttpPost.GetAllPostByJWT(this.Token)).subscribe(
      (response)=>{this.ArrAllPost=response}
    );
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

  async ClickDelete(PostIdToDelete?:number)
  {
    (await this.HttpPost.DeletePostById(this.Token,Number(PostIdToDelete))).subscribe(
      (response)=>{this.GetAllPostByJWT()}
    );
  }

  CloseTheSun()
  {
    this.OpenSun=0;
  }
}
