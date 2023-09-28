import { Component, OnInit } from '@angular/core';
import diaryData from './desktop.json';
import {Router} from "@angular/router";

@Component({
  selector: 'app-desktop',
  templateUrl: './desktop.component.html',
  styleUrls: ['./desktop.component.scss']
})
export class DesktopComponent implements OnInit {
  private page: number = 0;
  diary: any = diaryData[this.page];

  constructor(private router: Router) { }

  ngOnInit(): void {
  }


  nextPage() {
    if(this.page <= 17){
      this.page++;
      this.diary = diaryData[this.page];
    }else{
      this.diary = diaryData[this.page];
    }
  }

  previousPage() {
    if(this.page >= 0){
      this.page--;
      this.diary = diaryData[this.page];
    }else{
      this.diary = diaryData[this.page];
    }
  }

  chat() {
    this.router.navigate(['/chat'])
  }

}
