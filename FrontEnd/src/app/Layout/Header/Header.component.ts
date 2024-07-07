import { Component, Input, OnInit } from '@angular/core';
import { NavBarMenuItem } from 'src/app/Model/NavBarMenuItem';

@Component({
  selector: 'app-Header',
  templateUrl: './Header.component.html',
  styleUrls: ['./Header.component.css']
})
export class HeaderComponent implements OnInit {
@Input() HeaderNavBarData:Array<NavBarMenuItem>=[];
public str:string|undefined="Home";
  constructor() { }

  ngOnInit() {

  }

  // ActiveNavFunc()
  // {
  //  let ele =document.getElementById(""+this.str);
  //  ele?.classList.add("text-white");
  // }


}
