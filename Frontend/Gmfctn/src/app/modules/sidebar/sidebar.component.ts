import { Component, OnInit, Input,NgModule} from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  opened: boolean = false;
  @Input() user = {name:'',surname:''}; 
}
