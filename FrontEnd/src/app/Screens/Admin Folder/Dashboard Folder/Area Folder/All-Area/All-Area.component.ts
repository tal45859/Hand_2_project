import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { Area } from 'src/app/Model/Area';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { AreaService } from 'src/app/Services/Area.service';
import { AreaValidationService } from 'src/app/Services/AreaValidation.service';
import { LoginService } from 'src/app/Services/Login.service';

@Component({
  selector: 'app-All-Area',
  templateUrl: './All-Area.component.html',
  styleUrls: ['./All-Area.component.css']
})
export class AllAreaComponent implements OnInit {
  public Token:string="";
  public AllAreaArr:Array<Area>=[];
  public ObjToUpdateArea:Area={};
  public page:number=0;
  public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:'אנא הזן קוד סביבה! '}];
  public Select:number=1;
  public AreaIdForDelete?:number;
  public OpenUpdate:string="";


  constructor(private HttpLogin:LoginService,private HttpArea:AreaService,private Validation:AreaValidationService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.GetAllArea();

  }

  async GetAllArea()
  {
    (await this.HttpArea.GetAllArea()).subscribe((response)=>{this.AllAreaArr=response,this.page=0});
  }

  async ClickUpdateArea()
  {
    this.ResponseMessage[1].Isok=true;
    this.ResponseMessage[0]=this.Validation.CheckAreaName(this.ObjToUpdateArea.AreaName);
    this.ResponseMessage[1].Isok=this.ObjToUpdateArea.TopAreaId!=undefined&&this.ObjToUpdateArea.TopAreaId!=0;

    if(!this.ResponseMessage[0].Isok||!this.ResponseMessage[1].Isok)
    {
      return;
    }
    (await this.HttpArea.UpdateArea(this.Token,this.ObjToUpdateArea)).subscribe(
      (response)=>{this.OpenUpdate="",this.GetAllArea()},
      (error)=>{}
    )

  }

  async ClickDeleteArea()
  {
    (await this.HttpArea.DeleteAreaById(this.Token,Number(this.AreaIdForDelete))).subscribe(
      (response)=>{this.GetAllArea()},
      (error)=>{}
    )
  }

  async GetAreaByTopAreaId(TopAreaId?:number)
  {

    (await this.HttpArea.GetAllAreaByTopAreaId(Number(TopAreaId))).subscribe(
      (response)=>{this.AllAreaArr=response,this.page=0}
    );
  }


}
