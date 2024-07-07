import { Component, OnInit } from '@angular/core';
import { Area } from 'src/app/Model/Area';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { AreaService } from 'src/app/Services/Area.service';
import { AreaValidationService } from 'src/app/Services/AreaValidation.service';
import { LoginService } from 'src/app/Services/Login.service';

@Component({
  selector: 'app-Add-Area',
  templateUrl: './Add-Area.component.html',
  styleUrls: ['./Add-Area.component.css']
})
export class AddAreaComponent implements OnInit {
  public Token:string="";
  public ObjToAddArea:Area={};
  public Spiner:boolean=false;
  public IsCreate:boolean=false;
  public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:'אנא הזן קוד סביבה'}];

  constructor(private HttpLogin:LoginService,private HttpArea:AreaService,private Validation:AreaValidationService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
  }


  async ClickAddArea()
  {
    this.Spiner=true;
    this.ResponseMessage[1].Isok=true;
    this.ResponseMessage[0]=this.Validation.CheckAreaName(this.ObjToAddArea.AreaName);
    this.ResponseMessage[1].Isok=this.ObjToAddArea.TopAreaId!=undefined&&this.ObjToAddArea.TopAreaId!=0;
    if(!this.ResponseMessage[0].Isok||!this.ResponseMessage[1].Isok)
    {
      this.Spiner=false;
      return;
    }
    (await this.HttpArea.AddArea(this.Token,this.ObjToAddArea)).subscribe(
      (response)=>{this.Spiner=false,this.IsCreate=true,this.ObjToAddArea.AreaName='',this.ObjToAddArea.TopAreaId=undefined,this.AfterJoin()},
      (error)=>{}
    )
  }

  public AfterJoin()
  {
    setTimeout(() => {
      this.IsCreate=false;
    }, 1500);
  }





}
