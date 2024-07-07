import { Component, Input, OnInit } from '@angular/core';
import { Post } from 'src/app/Model/Post';

@Component({
  selector: 'app-Update-PostHome',
  templateUrl: './Update-PostHome.component.html',
  styleUrls: ['./Update-PostHome.component.css']
})
export class UpdatePostHomeComponent implements OnInit {

  public OpenNextStep:number=1;
  @Input()PostObjForUpdate?:Post;
    constructor() { }

    ngOnInit() {
    }


  NextStep(ev:number)
    {
      this.OpenNextStep=2;
    }
}
