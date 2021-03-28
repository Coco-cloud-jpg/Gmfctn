import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'greeting',
  templateUrl: './greeting.component.html',
  styleUrls: ['./greeting.component.scss']
})
export class GreetingComponent implements OnInit {
  greeting: string = '';
  @Input() user = {name:'',surname:''};
  constructor() { }

  ngOnInit(): void {
    let time = new Date().getHours();
    if(time>=5){
      if(time>=12){
        if(time>=18){
          this.greeting = 'Good evening, ';
        }
        else{
          this.greeting = 'Good day, ';
        }
      }
      else{
        this.greeting = 'Good morning, ';
      }
    }
    else{
      this.greeting = 'Good night, ';
    }
  }

}
