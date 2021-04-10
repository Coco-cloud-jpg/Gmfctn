import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-badges',
  templateUrl: './badges.component.html',
  styleUrls: ['./badges.component.scss']
})
export class BadgesComponent implements OnInit {
  public height = 0;
  public margin = '';
  public user: User = {name: 'Aezakmi', surname: 'Houston', total: 0, icon: '', status: '', email: 'asdqweqw@gmail.com'};

  ngOnInit(): void {
    this.calculateSize();
  }

  calculateSize(): void {
    const toolbarHeight = 75;
    const rowsCount = 2;
    const offsetForMargin = 10;

    this.height = ( document.body.clientHeight - toolbarHeight ) / rowsCount - 2 * offsetForMargin;
    this.margin = document.body.clientHeight / 100 + offsetForMargin + '';
  }
}
