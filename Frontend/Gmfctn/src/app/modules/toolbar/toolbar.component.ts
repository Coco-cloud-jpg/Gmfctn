import { Component, OnInit, Output, EventEmitter, Input} from '@angular/core';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  @Output() opened = new EventEmitter<boolean>();
  @Input() isOpened:boolean = false;
  @Input() user = {name:'',surname:''}; 
  open() {
    this.opened.emit(!this.isOpened);
  }
}
