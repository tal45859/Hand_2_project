// export class NavBarItems {

import { NavBarMenuItem } from "../Model/NavBarMenuItem";

export const Defult:Array<NavBarMenuItem>=[
  {
    Name:'דף הבית',
    Url:'Home'
  },
  // {
  //   Name:'מודעות',
  //   Url:'PostHome'
  // },
  {
    Name:'התחברות',
    Url:'Login'
  },
  {
    Name:'הרשמה',
    Url:'SingUp'
  }
];

export const Classic:Array<NavBarMenuItem>=[
  {
    Name:'דף הבית',
    Url:'Home'
  },
  // {
  //   Name:'מודעות',
  //   Url:'PostHome'
  // },
  {
    Name:'מועדפים',
    Url:'FavoriteHome'
  },
  {
    Name:'פרטים אישים',
    Url:'UserHome'
  },
];
export const Admin:Array<NavBarMenuItem>=[
  {
    Name:'דף הבית',
    Url:'Home'
  },
  // {
  //   Name:"מודעות",
  //   Url:'PostHome'
  // },
  {
    Name:'פרטים אישים',
    Url:'UserHome'
  },
  {
    Name:'ניהול משתמשים',
    Url:'AllUserHome'
  },
  {
    Name:'ניהול האתר',
    Url:'DashboardHome'
  },
  {
    Name:'סטטיסטיקות',
    Url:'Statistics'
  }
];
