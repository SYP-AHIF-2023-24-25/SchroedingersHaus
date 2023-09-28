import { Component, OnInit } from '@angular/core';
import {
  trigger, state, style, animate, transition, query, group
} from '@angular/animations';
import {Router} from "@angular/router";

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss'],

  animations: [
    trigger('fade', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 1 })),

      transition(':enter, :leave', [
        animate(1000)
      ])
      // transition(':enter', [
      //   style({ opacity: 0 }),
      //   animate(1000, style({ opacity: 1 }))
      // ]),
      // transition(':leave', [
      //   style({ opacity: 1 }),
      //   animate(1000, style({ opacity: 0 }))
      // ])
    ]),
    trigger('nope', [
      state('void', style({ opacity: 0 })),
      state('*', style({ opacity: 0 })),

      transition(':enter, :leave', [
        animate(5000)
      ])
      // transition(':enter', [
      //   style({ opacity: 0 }),
      //   animate(1000, style({ opacity: 1 }))
      // ]),
      // transition(':leave', [
      //   style({ opacity: 1 }),
      //   animate(1000, style({ opacity: 0 }))
      // ])
    ]),
    trigger('fadeSlide', [
      transition(':enter', [
        group([
          query('.item:nth-child(odd)', [
            style({ opacity: 0, transform: 'translateX(-250px)' }),
            animate(
              1000,
              style({ opacity: 1, transform: 'translateX(0)' })
            )
          ]),
          query('.item:nth-child(even)', [
            style({ opacity: 0, transform: 'translateX(250px)' }),
            animate(
              2000,
              style({ opacity: 1, transform: 'translateX(0)' })
            )
          ])
        ])
      ]),

      transition(':leave', [
        group([
          query('.item:nth-child(odd)', [
            animate(
              2000,
              style({ opacity: 0, transform: 'translateX(-250px)' })
            )
          ]),
          query('.item:nth-child(even)', [
            animate(
              2000,
              style({ opacity: 0, transform: 'translateX(250px)' })
            ),
          ])
        ])
      ])
    ])
  ]
})

export class StartComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }


  showEvents1 = true
  showEvents2 = true
  toggleFlag = false

  toggleEvents1() {
    if(!this.toggleFlag) {
      this.showEvents1 = !this.showEvents1
    }
  }

  toggleEvents2() {
    if(this.toggleFlag) {
      this.showEvents2 = !this.showEvents2
    }
  }

  showDiary(e: { keyCode?: number; preventDefault?: any; }) {
    e.preventDefault();
   this.router.navigate(['/diary']);
  }

  handleKeyUp(e: { keyCode: number; }){
    if(e.keyCode === 13){
      this.showDiary(e);
    }
  }
}


