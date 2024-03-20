import { Component, OnInit } from '@angular/core';
import diaryData from './diary.json';
import {Router} from "@angular/router";


@Component({
  selector: 'app-diary',
  templateUrl: './diary.component.html',
  styleUrls: ['./diary.component.scss']
})
export class DiaryComponent {
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
    this.router.navigate(['..'])
  }
}
