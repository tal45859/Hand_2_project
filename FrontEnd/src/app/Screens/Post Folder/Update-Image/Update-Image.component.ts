import { Component, Input, OnInit } from '@angular/core';
import { Image } from 'src/app/Model/Image';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { ImageService } from 'src/app/Services/Image.service';
import { ImageValidationService } from 'src/app/Services/ImageValidation.service';
import { LoginService } from 'src/app/Services/Login.service';

@Component({
  selector: 'app-Update-Image',
  templateUrl: './Update-Image.component.html',
  styleUrls: ['./Update-Image.component.css']
})
export class UpdateImageComponent implements OnInit {

  @Input()PostId?:number;
  public  formData = new FormData();
  public AllNewImageArr:Array<Image>=[];
  public CounterLengthImages=0;
  public ResponseMessage:ResponseValidation={Isok:true,Message:''};


  constructor(private HttpLogin:LoginService, private HttpImage:ImageService,private ImageValidation:ImageValidationService) { }

  ngOnInit() {
    this.GetAllImage();
  }

  public uploadFile = (files:any) => {
    this.ResponseMessage={Isok:true,Message:''};
    if (files.length === 0) {
      return;
    }
    if(this.CounterLengthImages==3)
    {
      this.ResponseMessage={Isok:false,Message:'ניתן לעלות 3 תמונות בלבד'};
      this.MessageError();
      return;
    }
    let fileToUpload = <File>files[0];
    this.formData = new FormData();
    this.formData.append('file', fileToUpload, this.PostId?.toString()+fileToUpload.name);
    this.ResponseMessage =  this.ImageValidation.CheckImage(fileToUpload.name);
    if(!this.ResponseMessage.Isok)
    {
      this.MessageError();
      return;
    }
     this.AddImageToFolder(this.PostId?.toString()+fileToUpload.name);
  }

  async AddImageToFolder(filename:string)
  {
      let objAddImageForDb:Image={};
      objAddImageForDb.Url=filename;
      objAddImageForDb.PostId=this.PostId;
      (await this.HttpImage.AddImageToFolder(this.formData,this.HttpLogin.Token)).subscribe(
         ()=>{this.AddImageToDB(objAddImageForDb)}
        ,()=>{this.ResponseMessage={Isok:false,Message:'לא הצלחנו להוסיף את התמונה אנא נסה שנית מאוחר יותר!'},this.MessageError();});//לעשות סיט טים אווט
  }

  async AddImageToDB(objToAdd:Image)
  {
    (await this.HttpImage.AddImageToDB(this.HttpLogin.Token,objToAdd)).subscribe(
      ()=>{this.CounterLengthImages++,this.GetAllImage()},
      ()=>{this.ResponseMessage={Isok:false,Message:'לא הצלחנו להוסיף את התמונה אנא נסה שנית מאוחר יותר!'},this.MessageError();}
    );
  }

  async GetAllImage()
  {
   (await this.HttpImage.GetAllImageByPostId(Number(this.PostId))).subscribe((response)=>{this.AllNewImageArr=response});
  }

  async OnclickDeleteImage(id?:number)
  {
    (await this.HttpImage.DeleteImageForUser(this.HttpLogin.Token,Number(id))).subscribe(()=>{this.CounterLengthImages--,this.GetAllImage()},
    ()=>{this.ResponseMessage={Isok:false,Message:'לא הצלחנו למחוק את התמונה אנא נסה שנית מאוחר יותר'},this.MessageError();});
  }

  public OnClickFinish()
  {
    window.location.reload();
  }

  public MessageError()
  {
    setTimeout(()=>{
      this.ResponseMessage={Isok:true,Message:''};
    },3000);
  }

}
