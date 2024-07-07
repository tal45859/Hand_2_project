import { Category } from './../../../Model/Category';
import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/Services/Category.service';
import { LoginService } from 'src/app/Services/Login.service';

@Component({
  selector: 'app-Post-Home',
  templateUrl: './Post-Home.component.html',
  styleUrls: ['./Post-Home.component.css']
})
export class PostHomeComponent implements OnInit {

  public Token:string="";

  public CategoryId?:number=0;
  public ArrAllCategory:Array<Category>=[];
  constructor(private HttpLogin:LoginService,private HttpCategory:CategoryService) { }

  ngOnInit() {
    this.Token=this.HttpLogin.Token;
    this.GetALlCategory();
  }

  async GetALlCategory()
  {
    (await this.HttpCategory.GetAllCategory()).subscribe((response)=>{this.ArrAllCategory=response})
  }

}
