import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { User } from 'src/app/Model/User';
import { UserService } from 'src/app/Services/User.service';
import { UserValidationService } from 'src/app/Services/UserValidation.service';

@Component({
  selector: 'app-Sign-up',
  templateUrl: './Sign-up.component.html',
  styleUrls: ['./Sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  public UserObj:User={};
  public spinner = false;
  public IsCreate = false;
  public ShowPassword = false;
  public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''}
                                                   ,{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''}];

  constructor(private HttpUser:UserService,private router:Router,private UserValidation:UserValidationService) { }

  ngOnInit() {
  }

  async ClickSignUp()
  {
    this.spinner=true;
    this.ResponseMessage[0] = this.UserValidation.CheckFirstName(this.UserObj.FirstName);
    this.ResponseMessage[1] = this.UserValidation.CheckLastName(this.UserObj.LastName);
    this.ResponseMessage[2] = this.UserValidation.CheckEmail(this.UserObj.Mail);
    this.ResponseMessage[3] = this.UserValidation.CheckBirthdate(this.UserObj.Birthdate);
    this.ResponseMessage[4] = this.UserValidation.CheckPhone(this.UserObj.Phone);
    this.ResponseMessage[5] = this.UserValidation.CheckPassword(this.UserObj.Password);
    if(!this.ResponseMessage[0].Isok||!this.ResponseMessage[1].Isok||!this.ResponseMessage[2].Isok
      ||!this.ResponseMessage[3].Isok||!this.ResponseMessage[4].Isok||!this.ResponseMessage[5].Isok)
    {
      this.spinner=false;
      return;
    }
    (await this.HttpUser.GetHaveUser(String(this.UserObj.Mail))).subscribe(
      (response)=>{this.AddUser()}
      ,(error)=>{this.spinner=false,this.ResponseMessage[2]={Isok:false,Message:'מייל קיים במערכת אנא נסה מייל אחר!'}});
  }

  async AddUser()
  {
    (await this.HttpUser.AddUser(this.UserObj)).subscribe(
      (response)=>{this.spinner=false, this.GoToLogin()},(error)=>{this.spinner=false}
    );
  }

  public GoToLogin()
  {
    this.IsCreate=true;
    setTimeout(()=>{ this.router.navigate(['/Login'])
    .then(() => {
      window.location.reload();
    });},3000);
  }



}
