import { Component, Input, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/Login.service';
import { PostService } from 'src/app/Services/Post.service';

@Component({
  selector: 'app-Post-Statistics',
  templateUrl: './Post-Statistics.component.html',
  styleUrls: ['./Post-Statistics.component.css']
})
export class PostStatisticsComponent implements OnInit {

  public Token:string="";
  public PrecentageClassicUser:number=0;
  public ArrCategoryText:Array<string>=['ניתוח מודעות חדשות היום','ניתוח מודעות חדשות החודש','ניתוח מודעות חדשות השנה'];
  @Input() TypeDetails:number=0;
  constructor(private HttpLogin:LoginService,private HttpPost:PostService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.GetAllPrecentagePostUploadByDate();
  }


  async GetAllPrecentagePostUploadByDate()
  {
    //כמה מודעות נתוך כל המודעות עלו היום החודש והשנה
    let ArrDate:Array<string>=["Today","Month","Year"];
    (await this.HttpPost.GetAllPrecentagePostUploadByDate(this.Token,ArrDate[this.TypeDetails])).subscribe(
      (response)=>{this.PrecentageClassicUser=response});
  }

}
