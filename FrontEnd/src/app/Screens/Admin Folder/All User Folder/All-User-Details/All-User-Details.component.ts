import { Component, OnInit } from '@angular/core';
import { ChangeUserRole } from 'src/app/Model/ChangeUserRole';
import { User } from 'src/app/Model/User';
import { LoginService } from 'src/app/Services/Login.service';
import { UserService } from 'src/app/Services/User.service';

@Component({
  selector: 'app-All-User-Details',
  templateUrl: './All-User-Details.component.html',
  styleUrls: ['./All-User-Details.component.css']
})
export class AllUserDetailsComponent implements OnInit {
  public Token:string="";
  public UserArray:Array<User>=[];
  public Select=1;
  public page:number=0;
  public UserObjToDelete:User={};
  public ChangeUserRoleObj:ChangeUserRole={};
  constructor(private HttpLogin:LoginService, private HttpUser:UserService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.GetAllUser();

  }

  async GetAllUser()
  {
    (await this.HttpUser.GetAllUsers(this.Token)).subscribe((response)=>{this.UserArray=response},(error)=>{});
  }

  async GetAllAdmin()
  {
    (await this.HttpUser.GetAllAdmin(this.Token)).subscribe((response)=>{this.UserArray=response},(error)=>{});
  }

  async GetAllClassicUser()
  {
    (await this.HttpUser.GetAllUserNotAdmin(this.Token)).subscribe((response)=>{this.UserArray=response},(error)=>{});
  }

  async ClickDeleteUser()
  {
    (await this.HttpUser.DeleteUserByIdForAdmin(this.Token,Number(this.UserObjToDelete.Id))).subscribe(
      (response)=>{this.RestAfterChange()},(error)=>{});
      //לעשות משהו כמו בהודעת שגיאה
  }

  async ClickChangeRole()
  {
    (await this.HttpUser.ChangeUserRoleForAdmin(this.Token,this.ChangeUserRoleObj)).subscribe((response)=>{this.RestAfterChange()},(error)=>{});
  }

  public RestAfterChange()
  {
    if(this.Select==1)
    {
      this.GetAllUser();
    }
    else if(this.Select==2)
    {
      this.GetAllAdmin();
    }
    else
    {
      this.GetAllClassicUser();
    }
  }

}
