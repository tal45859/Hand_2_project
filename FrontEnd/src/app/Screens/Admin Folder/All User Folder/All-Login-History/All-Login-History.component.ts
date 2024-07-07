import { LoginService } from 'src/app/Services/Login.service';
import { LoginHistory } from './../../../../Model/LoginHistory';
import { Component, OnInit } from '@angular/core';
import { LoginHistoryService } from 'src/app/Services/LoginHistory.service';
import { User } from 'src/app/Model/User';
import { UserService } from 'src/app/Services/User.service';

@Component({
  selector: 'app-All-Login-History',
  templateUrl: './All-Login-History.component.html',
  styleUrls: ['./All-Login-History.component.css']
})
export class AllLoginHistoryComponent implements OnInit {

  public Select=1;
  public page:number=0;
  public Token:string="";
  public LoginHistoryArray:Array<LoginHistory>=[];
  public UserObjToDelete:User|undefined={};
  constructor(private HttpLogin:LoginService,private HttpLoginHistory:LoginHistoryService,private HttpUser:UserService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.GetAllLoginHistory("AllTheTime");
  }

  async GetAllLoginHistory(Date:string)
  {
    //Today=היום   Month=החודש   AllTheTime=כל הזמנים   Year= השנה
    (await this.HttpLoginHistory.GetLoginHistoryFilteringByDate(this.Token,Date)).subscribe((response)=>{this.LoginHistoryArray=response},(error)=>{});
  }

  async ClickDeleteUser()
  {
    (await this.HttpUser.DeleteUserByIdForAdmin(this.Token,Number(this.UserObjToDelete?.Id))).subscribe(
      (response)=>{this.RestAfterChange()},(error)=>{});
      //לעשות משהו כמו בהודעת שגיאה
  }

  public RestAfterChange()
  {
    if(this.Select==1)
    {
      this.GetAllLoginHistory('AllTheTime');
    }
    else if(this.Select==2)
    {
      this.GetAllLoginHistory('Year');
    }
    else if(this.Select==3)
    {
      this.GetAllLoginHistory('Month');
    }
    else
    {
      this.GetAllLoginHistory('Today')
    }
  }

}
