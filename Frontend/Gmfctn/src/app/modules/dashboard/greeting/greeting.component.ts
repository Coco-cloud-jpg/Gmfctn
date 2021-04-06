import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-greeting',
  templateUrl: './greeting.component.html',
  styleUrls: ['./greeting.component.scss']
})
export class GreetingComponent implements OnInit {

  @Input() user = {name: '', surname: ''};

  greeting = '';

  ngOnInit(): void {
    this.setGreeting();
  }

  private setGreeting(): void {
    const time = new Date().getHours();
    if ( time < 5) {
      this.greeting = 'Good night';
    } else if ( time < 12) {
      this.greeting = 'Good morning';
    } else if ( time < 18) {
      this.greeting = 'Good day';
    } else {
      this.greeting = 'Good evening';
    }
  }
}
