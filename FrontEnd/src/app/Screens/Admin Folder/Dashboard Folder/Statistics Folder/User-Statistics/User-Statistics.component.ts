import { Component, Input, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/Login.service';
import { LoginHistoryService } from 'src/app/Services/LoginHistory.service';
import { PostService } from 'src/app/Services/Post.service';
import { UserService } from 'src/app/Services/User.service';

@Component({
  selector: 'app-User-Statistics',
  templateUrl: './User-Statistics.component.html',
  styleUrls: ['./User-Statistics.component.css']
})
export class UserStatisticsComponent implements OnInit {
  public Token:string="";
  public PrecentageClassicUser:number=0;
  public ArrCategoryText:Array<string>=['ניתוח סוגי משתמשים','ניתוח פעילים היום','ניתוח פעילים החודש','ניתוח פעילים השנה'];
  @Input() TypeDetails:number=0;

  constructor(private HttpLogin:LoginService,private HttpUser:UserService,private HttpLoginhistory:LoginHistoryService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    if(this.TypeDetails==0)
    {
      this.GetAllPrecentageClassicUser();
    }
    else
    {
      this.GetAllPrecentageLoginHistoryUser();
    }
  }

  //כמה מנהלים וכמה משתמשים רגילים
  async GetAllPrecentageClassicUser()
  {
    (await this.HttpUser.GetAllPrecentageClassicUser(this.Token)).subscribe(
      (response)=>{this.PrecentageClassicUser=response}
    )
  }

  async GetAllPrecentageLoginHistoryUser()
  {
    let ArrDate:Array<string>=["Today","Month","Year"];
    (await this.HttpLoginhistory.GetAllPrecentageLoginHistoryUserByDate(this.Token,ArrDate[this.TypeDetails-1])).subscribe(
      (response)=>{this.PrecentageClassicUser=response}
    );
  }

}
