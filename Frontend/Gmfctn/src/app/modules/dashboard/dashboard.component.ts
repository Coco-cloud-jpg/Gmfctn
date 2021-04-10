import { Component, OnInit } from '@angular/core';
import '../../models/user';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public height = 0;
  public margin = '';
  public user: User = {name: 'Aezakmi', surname: 'Houston', total: 0, icon: '', status: '', email: 'asdqweqw@gmail.com'};

  ngOnInit(): void {
    this.calculateSize();
  }

  calculateSize(): void {
    const toolbarHeight = 75;
    const rowsCount = 7;
    const offsetForMargin = 10;

    this.height = ( document.body.clientHeight - toolbarHeight ) / rowsCount - 2 * offsetForMargin;
    this.margin = document.body.clientHeight / 100 + offsetForMargin + '';
  }
}
