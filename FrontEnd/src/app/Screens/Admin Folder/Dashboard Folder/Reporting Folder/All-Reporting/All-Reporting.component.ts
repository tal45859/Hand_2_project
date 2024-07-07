import { Component, Input, OnInit } from '@angular/core';
import { Post } from 'src/app/Model/Post';
import { Reporting } from 'src/app/Model/Reporting';
import { LoginService } from 'src/app/Services/Login.service';
import { PostService } from 'src/app/Services/Post.service';
import { ReportingService } from 'src/app/Services/Reporting.service';

@Component({
  selector: 'app-All-Reporting',
  templateUrl: './All-Reporting.component.html',
  styleUrls: ['./All-Reporting.component.css']
})
export class AllReportingComponent implements OnInit {
  @Input()SelectType?:number;
  public ReportingArr:Array<Reporting>=[];
  public DesBefElmentID?:number=0;
  public page=1;
  public Token:string="";
  public ReportingItemForUpdate:Reporting={};
  public OpenSun=0;
  public PostObjToSun:Post={};

  constructor(private HttpLogin:LoginService,private HttpReporting:ReportingService,private HttpPost:PostService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    if(this.SelectType==1)
      {
        this.GetAllReporting();
      }
      else if(this.SelectType==2)
        {
          this.GetAllReportingActive();
        }
        else if(this.SelectType==3)
          {
            this.GetAllReportingNoActive();
          }


  }

  async GetAllReporting()
  {
    (await this.HttpReporting.GetAllReporting(this.Token)).subscribe(
      (response)=>{this.ReportingArr=response}
    );
  }

  async GetAllReportingActive()
  {
    (await this.HttpReporting.GetAllReportingActive(this.Token)).subscribe(
      (response)=>{this.ReportingArr=response}
    );
  }

  async GetAllReportingNoActive()
  {
    (await this.HttpReporting.GetAllReportingNoActive(this.Token)).subscribe(
      (response)=>{this.ReportingArr=response}
    );
  }

  async DeleteReporting(ReportId?:number)
  {
    (await this.HttpReporting.DeleteReportingById(this.Token,Number(ReportId))).subscribe(
      (response)=>{window.location.reload()},
      (error)=>{}
    )
  }

  async UpdateReportingItem()
  {
    this.ReportingItemForUpdate.IsActive=true;
    (await this.HttpReporting.UpdateReporting(this.Token,this.ReportingItemForUpdate)).subscribe(
      (response)=>{window.location.reload()},
      (error)=>{}
    )
  }

  async GetPostByIDForSun(PostId?:number)
  {
    (await this.HttpPost.GetPostById(Number(PostId))).subscribe((response)=>{this.PostObjToSun=response,this.OpenSun=1});
  }


  public ClickOpenSub(ItemId?:number)
  {
    const ClassPanel= document?.getElementById(""+ItemId)?.classList;
    if(ClassPanel?.toString() == "removeDisplay")//אם הוא שווה לו תפתח
    {
      document?.getElementById(""+ItemId)?.classList.remove('removeDisplay');
      document?.getElementById(""+ItemId)?.classList.add('AddDisplay');
      // //להוריד את המחק והערוך לקטגוריה
      // document?.getElementById("Reporting"+ItemId)?.classList.add('removeDisplay');

    }
    else //אם הוא שונה תסגור
    {
      document?.getElementById(""+ItemId)?.classList.remove('AddDisplay');
      document?.getElementById(""+ItemId)?.classList.add('removeDisplay');
      // //להוסיף את המחק והערוך לקטגוריה
      // document?.getElementById("Reporting"+ItemId)?.classList.remove('removeDisplay');
    }

      // //למחוק לפאנל הקודם את האפשרות להציג
      if(ItemId!=this.DesBefElmentID)
      {
        document?.getElementById(""+this.DesBefElmentID)?.classList.remove('AddDisplay');
        document?.getElementById(""+this.DesBefElmentID)?.classList.add('removeDisplay');
        // document?.getElementById("Reporting"+this.DesBefElmentID)?.classList.remove('removeDisplay');
        this.DesBefElmentID=ItemId;
      }
  }

  CloseTheSun()
  {
    this.OpenSun=0;
  }

}
