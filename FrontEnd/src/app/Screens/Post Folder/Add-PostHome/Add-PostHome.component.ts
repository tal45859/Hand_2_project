import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-Add-PostHome',
  templateUrl: './Add-PostHome.component.html',
  styleUrls: ['./Add-PostHome.component.css']
})
export class AddPostHomeComponent implements OnInit {
public PostId?:number;
public OpenNextStep:number=1;
  constructor() { }

  ngOnInit() {
  }


NextStep(LastPostId:number)
  {
    this.PostId=LastPostId;
    this.OpenNextStep=2;
  }

}
