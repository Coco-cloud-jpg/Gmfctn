import { Component, OnInit, Input } from '@angular/core';
import '../../models/user';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public height = 0;
  public margin = '';
  public user: User = {name: 'Aezakmi', surname: 'Houston', total: 0, icon: ''};

  ngOnInit(): void {
    this.calculateSize();
  }

  calculateSize(): void {
    const toolbarHeight = 200;
    const rowsCount = 7;

    this.height = ( document.body.clientHeight - toolbarHeight ) / rowsCount;
    this.margin = this.height / rowsCount + '';
  }
}
