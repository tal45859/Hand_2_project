import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Image } from 'src/app/Model/Image';
import { Post } from 'src/app/Model/Post';
import { ImageService } from 'src/app/Services/Image.service';
import { PostService } from 'src/app/Services/Post.service';

@Component({
  selector: 'app-Post-Details',
  templateUrl: './Post-Details.component.html',
  styleUrls: ['./Post-Details.component.css']
})
export class PostDetailsComponent implements OnInit {
  @Input() PostObj?:Post={};
  @Output() CloseMeOutpot = new EventEmitter<any>();
  public ImageArray:Array<Image>=[];
  public AddLike:boolean=false;


  public Index:number=0;
  constructor(private HttpImage:ImageService,private HttpPost:PostService) { }

  ngOnInit() {
   this.GetAllImageByRecipeId();
   this.AddShow()
  }

  async GetAllImageByRecipeId()
  {
    (await this.HttpImage.GetAllImageByPostId(Number(this.PostObj?.Id))).subscribe((response)=>{this.ImageArray=response});
  }

  async AddShow()
  {
    (await this.HttpPost.AddViewsToPost(Number(this.PostObj?.Id))).subscribe();
  }

  public ChangeIndex(isNextOrPrev:boolean)
  {
    if(isNextOrPrev)
    {
      if(this.Index==this.ImageArray.length-1)
      {
        this.Index=0;
      }
      else
      {
        this.Index++;
      }
    }
    else
    {
      if(this.Index==0)
      {
        this.Index=this.ImageArray.length-1;
      }
      else
      {
        this.Index--;
      }
    }
  }

  public ClickClose()
  {
    this.CloseMeOutpot.emit();
  }

}
