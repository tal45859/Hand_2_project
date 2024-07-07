import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { User } from 'src/app/Model/User';
import { LoginService } from 'src/app/Services/Login.service';
import { UserService } from 'src/app/Services/User.service';
import { UserValidationService } from 'src/app/Services/UserValidation.service';

@Component({
  selector: 'app-User-Details',
  templateUrl: './User-Details.component.html',
  styleUrls: ['./User-Details.component.css']
})
export class UserDetailsComponent implements OnInit {

  public OpenUpdate=true;
  public UserObj:User={};
  private Password:string|undefined="";
  public Token:string="";
  public responseMessage:Array<ResponseValidation>=[{Isok:true},{Isok:true},{Isok:true},{Isok:true},{Isok:true}];
  constructor(private Httplogin:LoginService,private HttpUser:UserService,private UserValid:UserValidationService,private router:Router) { }

  ngOnInit() {
    this.Token=this.Httplogin.Token;
    this.GetUserDetails();

  }

  async GetUserDetails()
  {
    (await this.HttpUser.GetUserByToken(this.Token)).subscribe(
      (response)=>{this.UserObj=response,this.Password=response.Password}
    );

  }

  async ClickDeleteUser()
  {
    this.responseMessage[4].Isok=true;
    (await this.HttpUser.DeleteUserByJWT(this.Token)).subscribe((response)=>{
      this.router.navigate(['/Home'])
      .then(()=>{window.location.reload()})
    },(error)=>{this.responseMessage[4]={Isok:false,Message:'לא ניתן למחוק אנא נסה שנית מאוחר יותר!'}});
  }

  async ClickUpdateUser()
  {
    this.responseMessage[4].Isok=true;
    this.responseMessage[0] = this.UserValid.CheckFirstName(this.UserObj.FirstName);
    this.responseMessage[1] = this.UserValid.CheckLastName(this.UserObj.LastName);
    this.responseMessage[2] = this.UserValid.CheckPhone(this.UserObj.Phone);
    if(this.Password!=this.UserObj.Password)
    {
      this.responseMessage[3] = this.UserValid.CheckPassword(this.UserObj.Password);
    }
    if(!this.responseMessage[0].Isok||!this.responseMessage[1].Isok||!this.responseMessage[2].Isok||!this.responseMessage[3].Isok)
    {
      return;
    }
    (await this.HttpUser.UpdateUserByJWT(this.Token,this.UserObj)).subscribe((response)=>{window.location.reload()},
    (error)=>{this.responseMessage[4]={Isok:false,Message:'לא ניתן לעדכן אנא נסה שנית מאוחר יותר!'}});

  }

}
