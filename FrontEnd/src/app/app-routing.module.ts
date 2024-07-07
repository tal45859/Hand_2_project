import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Screens/Login Folder/Login/Login.component';
import { SignUpComponent } from './Screens/Login Folder/Sign-up/Sign-up.component';
import { ForgotPasswordComponent } from './Screens/Login Folder/Forgot-Password/Forgot-Password.component';
import { UserDetailsComponent } from './Screens/User Folder/User-Details/User-Details.component';
import { AllUserHomeComponent } from './Screens/Admin Folder/All User Folder/All-User-Home/All-User-Home.component';
import { DashboardHomeComponent } from './Screens/Admin Folder/Dashboard Folder/Dashboard-Home/Dashboard-Home.component';
import { StatisticsHomeComponent } from './Screens/Admin Folder/Dashboard Folder/Statistics Folder/Statistics-Home/Statistics-Home.component';
import { PostHomeComponent } from './Screens/Post Folder/Post-Home/Post-Home.component';
import { AllFavoriteComponent } from './Screens/Favorite Folder/All-Favorite/All-Favorite.component';
import { AllMyPostComponent } from './Screens/Post Folder/All-My-Post/All-My-Post.component';
import { AdminGuardService } from './Services/AdminGuard.service';
import { ClassicUserGuardService } from './Services/ClassicUserGuard.service';
import { GeneralGuardService } from './Services/GeneralGuard.service';

const routes: Routes = [
  {path:'Login',component:LoginComponent},
  {path:'SingUp',component:SignUpComponent},
  {path:'ForgotPassword',component:ForgotPasswordComponent},
  {path:'UserHome',component:UserDetailsComponent,canActivate:[GeneralGuardService]},
  {path:'DashboardHome',component:DashboardHomeComponent,canActivate:[AdminGuardService]},
  {path:'AllUserHome',component:  AllUserHomeComponent,canActivate:[AdminGuardService]},
  {path:'Statistics',component:StatisticsHomeComponent,canActivate:[AdminGuardService]},
  {path:'Home',component:PostHomeComponent},
  {path:'FavoriteHome',component:AllFavoriteComponent,canActivate:[ClassicUserGuardService]},
  {path:'AllMyPost',component:AllMyPostComponent,canActivate:[GeneralGuardService]},
  {path:'**',component:PostHomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
