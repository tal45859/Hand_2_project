import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import {MatChipsModule} from '@angular/material/chips';
import { ContentComponent } from './Layout/Content/Content.component';
import { FooterComponent } from './Layout/Footer/Footer.component';
import { HeaderComponent } from './Layout/Header/Header.component';
import { LoginComponent } from './Screens/Login Folder/Login/Login.component';
import { ForgotPasswordComponent } from './Screens/Login Folder/Forgot-Password/Forgot-Password.component';
import { SignUpComponent } from './Screens/Login Folder/Sign-up/Sign-up.component';
import { UserDetailsComponent } from './Screens/User Folder/User-Details/User-Details.component';
import { AllUserHomeComponent } from './Screens/Admin Folder/All User Folder/All-User-Home/All-User-Home.component';
import { AllUserDetailsComponent } from './Screens/Admin Folder/All User Folder/All-User-Details/All-User-Details.component';
import { AllLoginHistoryComponent } from './Screens/Admin Folder/All User Folder/All-Login-History/All-Login-History.component';
import { DashboardHomeComponent } from './Screens/Admin Folder/Dashboard Folder/Dashboard-Home/Dashboard-Home.component';
import { StatisticsHomeComponent } from './Screens/Admin Folder/Dashboard Folder/Statistics Folder/Statistics-Home/Statistics-Home.component';
import { UserStatisticsComponent } from './Screens/Admin Folder/Dashboard Folder/Statistics Folder/User-Statistics/User-Statistics.component';
import { PostStatisticsComponent } from './Screens/Admin Folder/Dashboard Folder/Statistics Folder/Post-Statistics/Post-Statistics.component';
import { CategoryHomeComponent } from './Screens/Admin Folder/Dashboard Folder/Category Folder/Category-Home/Category-Home.component';
import { AllCategoryComponent } from './Screens/Admin Folder/Dashboard Folder/Category Folder/All-Category/All-Category.component';
import { AddCategoryComponent } from './Screens/Admin Folder/Dashboard Folder/Category Folder/Add-Category/Add-Category.component';
import { AddSubCategoryComponent } from './Screens/Admin Folder/Dashboard Folder/Category Folder/Add-SubCategory/Add-SubCategory.component';
import { AreaHomeComponent } from './Screens/Admin Folder/Dashboard Folder/Area Folder/Area-Home/Area-Home.component';
import { AddAreaComponent } from './Screens/Admin Folder/Dashboard Folder/Area Folder/Add-Area/Add-Area.component';
import { AllAreaComponent } from './Screens/Admin Folder/Dashboard Folder/Area Folder/All-Area/All-Area.component';
import {NgxPaginationModule} from 'ngx-pagination';
import { PostHomeComponent } from './Screens/Post Folder/Post-Home/Post-Home.component';
import { AllPostComponent } from './Screens/Post Folder/All-Post/All-Post.component';
import { AddPostHomeComponent } from './Screens/Post Folder/Add-PostHome/Add-PostHome.component';
import { AddPostComponent } from './Screens/Post Folder/Add-Post/Add-Post.component';
import { AddImageComponent } from './Screens/Post Folder/Add-Image/Add-Image.component';
import { PostDetailsComponent } from './Screens/Post Folder/Post-Details/Post-Details.component';
import { AllFavoriteComponent } from './Screens/Favorite Folder/All-Favorite/All-Favorite.component';
import { AllMyPostComponent } from './Screens/Post Folder/All-My-Post/All-My-Post.component';
import { UpdatePostHomeComponent } from './Screens/Post Folder/Update-PostHome/Update-PostHome.component';
import { UpdateImageComponent } from './Screens/Post Folder/Update-Image/Update-Image.component';
import { UpdatePostComponent } from './Screens/Post Folder/Update-Post/Update-Post.component';
import { ReportingHomeComponent } from './Screens/Admin Folder/Dashboard Folder/Reporting Folder/Reporting-Home/Reporting-Home.component';
import { AllReportingComponent } from './Screens/Admin Folder/Dashboard Folder/Reporting Folder/All-Reporting/All-Reporting.component';
@NgModule({
  declarations: [
    AppComponent,
    ContentComponent,
    FooterComponent,
    HeaderComponent,
    LoginComponent,
    ForgotPasswordComponent,
    SignUpComponent,
    UserDetailsComponent,
    AllUserHomeComponent,
    AllLoginHistoryComponent,
    AllUserDetailsComponent,
    DashboardHomeComponent,
    StatisticsHomeComponent,
    UserStatisticsComponent,
    PostStatisticsComponent,
    CategoryHomeComponent,
    AllCategoryComponent,
    AddCategoryComponent,
    AddSubCategoryComponent,
    AreaHomeComponent,
    AddAreaComponent,
    AllAreaComponent,
    PostHomeComponent,
    AllPostComponent,
    AddPostHomeComponent,
    AddPostComponent,
    AddImageComponent,
    PostDetailsComponent,
    AllFavoriteComponent,
    AllMyPostComponent,
    UpdatePostHomeComponent,
    UpdateImageComponent,
    UpdatePostComponent,
    ReportingHomeComponent,
    AllReportingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSlideToggleModule,
    MatChipsModule,
    MatMenuModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    NgxPaginationModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
