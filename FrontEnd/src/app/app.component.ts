import { Component } from '@angular/core';
import { NavBarMenuItem } from './Model/NavBarMenuItem';
import { Defult, Classic, Admin } from './NavBarData/NavBarItems';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'yad2';

  public RoleNav= sessionStorage.getItem("Navbar");
  DefultNavBar:Array<NavBarMenuItem>=Defult;
  ClassicNavBar:Array<NavBarMenuItem>=Classic
  AdminNavBar:Array<NavBarMenuItem>=Admin;

}
