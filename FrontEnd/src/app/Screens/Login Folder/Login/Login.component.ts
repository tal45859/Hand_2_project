import { UserService } from './../../../Services/User.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/app/Model/Login';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { LoginService } from 'src/app/Services/Login.service';
import { LoginHistoryService } from 'src/app/Services/LoginHistory.service';
import { UserValidationService } from 'src/app/Services/UserValidation.service';

@Component({
  selector: 'app-Login',
  templateUrl: './Login.component.html',
  styleUrls: ['./Login.component.css']
})
export class LoginComponent implements OnInit {

  public LoginObj:Login={};
  public Token:string="";
  public spiner=false;
  public OpenPassword=false;
  public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true}];
  constructor(private HttpUser:UserService,private httpLogin:LoginService, private HttpLoginHistory:LoginHistoryService, private valid:UserValidationService,private router:Router) { }

  ngOnInit() {
  }

  //להמשיך
  async ClickLogin()
  {
    this.spiner=true;
    this.ResponseMessage[0]=this.valid.CheckEmail(this.LoginObj.Email);
    this.ResponseMessage[1]=this.valid.CheckPassword(this.LoginObj.Password);
    this.ResponseMessage[2].Isok=true;
    if(!this.ResponseMessage[0].Isok||!this.ResponseMessage[1].Isok)
    {
      this.spiner=false;
      return;
    }
    (await this.httpLogin.auth(this.LoginObj)).subscribe(
      (response)=>{this.spiner=false,this.Token = response,this.httpLogin.Token=response, this.AddHttpLoginHistory(),this.GetUseByToken()
    },(error)=>{this.spiner=false,this.ResponseMessage[2]={Isok:false,Message:'מייל או סיסמה אינם נכונים!'}})
   }
//,this.GetUseByToken()

  async AddHttpLoginHistory()
  {
   (await this.HttpLoginHistory.AddLoginHistory(this.Token)).subscribe();
  }

  async GetUseByToken()
  {
    (await this.HttpUser.GetUserByToken(this.Token)).subscribe(
      (response)=>{this.httpLogin.Role=String(response.Role),
      this.httpLogin.Navbar=String(response.Role),
      this.router.navigate(['/Home'])
      .then(()=>{ window.location.reload();})})
  }



}

//לשלוח ולקבל תוקן
//לשלוח תוקן ולקבל משתמש ולשמור את הרול
//להוסיף לו להיסטוריה
//ולהעביר אותו לדף הבית עם רפרוש
//
