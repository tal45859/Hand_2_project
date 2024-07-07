import { Component, OnInit } from '@angular/core';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { UserService } from 'src/app/Services/User.service';
import { UserValidationService } from 'src/app/Services/UserValidation.service';

@Component({
  selector: 'app-Forgot-Password',
  templateUrl: './Forgot-Password.component.html',
  styleUrls: ['./Forgot-Password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
public Mail:string="";
public spinner=false;
public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:false,Message:'במידה והמייל קיים במערכת נשלחה לך סיסמת איפוס למייל'}];

  constructor(private HttpUser:UserService,private ValidUser:UserValidationService) { }

  ngOnInit() {
  }

  async ClickResetPassword()
  {
    this.spinner=true;
    this.ResponseMessage[1].Isok=false;
    this.ResponseMessage[0]=this.ValidUser.CheckEmail(this.Mail);
    if(!this.ResponseMessage[0].Isok)
    {
      this.spinner=false;
      return;
    }
    (await this.HttpUser.ForgotPassword(this.Mail));
    this.spinner=false;
    this.ResponseMessage[1].Isok=true;
  }





}
