import { Component, OnInit, Output, EventEmitter, Input} from '@angular/core';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {

  @Input() isOpened = false;
  @Input() user = { name: '', surname: ''};

  @Output() opened = new EventEmitter<boolean>();

  open(): void {
    this.opened.emit(!this.isOpened);
  }
}
