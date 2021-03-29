import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-greeting',
  templateUrl: './greeting.component.html',
  styleUrls: ['./greeting.component.scss']
})
export class GreetingComponent implements OnInit {

  @Input() user = {name: '', surname: ''};

  public greeting = '';

  ngOnInit(): void {
    const time = new Date().getHours();
    if ( time >= 5){
      if ( time >= 12){
        if ( time >= 18){
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
