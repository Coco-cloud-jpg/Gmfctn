import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SayThankModalComponent } from '../../../../shared/components/say-thank-modal/say-thank-modal.component';
import { Roles, User } from 'src/app/shared/models/user';
import { Graph } from 'src/app/shared/models/graph';

@Component({
  selector: 'app-top-chart',
  templateUrl: './top-chart.component.html',
  styleUrls: ['./top-chart.component.scss']
})
export class TopChartComponent implements OnInit {
  users: User[] = [{
                    firstName: 'Petro',
                    lastName: 'Poroshenko',
                    userName: 'Petya',
                    xp: 120,
                    avatarId: '../../../../assets/5.jpg',
                    status: '',
                    email: 'asdqweqw@gmail.com',
                    id: 'asdqwe',
                    roles: [Roles.User]
                  }, {
                    firstName: 'Petro',
                    lastName: 'Poroshenko',
                    userName: 'Petya',
                    xp: 1020,
                    avatarId: '../../../../assets/5.jpg',
                    status: '',
                    email: 'asdqweqw@gmail.com',
                    id: 'asdqwe',
                    roles: [Roles.User]
                  }, {
                    firstName: 'Petro',
                    lastName: 'Poroshenko',
                    userName: 'Petya',
                    xp: 100,
                    avatarId: '../../../../assets/5.jpg',
                    status: '',
                    email: 'asdqweqw@gmail.com',
                    id: 'asdqwe',
                    roles: [Roles.User]
                  }, {
                    firstName: 'Petro',
                    lastName: 'Poroshenko',
                    userName: 'Petya',
                    xp: 1,
                    avatarId: '../../../../assets/5.jpg',
                    status: '',
                    email: 'asdqweqw@gmail.com',
                    id: 'asdqwe',
                    roles: [Roles.User]
                  }, {
                    firstName: 'Petro',
                    lastName: 'Poroshenko',
                    userName: 'Petya',
                    xp: 10,
                    avatarId: '../../../../assets/5.jpg',
                    status: '',
                    email: 'asdqweqw@gmail.com',
                    id: 'asdqwe',
                    roles: [Roles.User]
                  }];
  bars: Graph[] = [{
                    Value: 0,
                    Color: 'rgb(42,122,163)',
                  }, {
                    Value: 0,
                    Color: 'rgb(117,66,147)',
                  }, {
                    Value: 0,
                    Color: 'rgb(212,142,39)',
                  }, {
                    Value: 0,
                    Color: 'rgb(34,237,233)',
                  }, {
                    Value: 0,
                    Color: 'rgb(240,214,96)',
                  } ];


  constructor(public dialog: MatDialog) {}

  ngOnInit(): void {
    this.calculateGraphsLength();
  }

  private calculateGraphsLength(): void {
    this.users.sort(( a, b) => b.xp - a.xp);
    const maxLength = this.users[0].xp;

    for ( let i = 0; i < this.bars.length; ++i){
      this.bars[i].Value = this.users[i].xp * 100 / maxLength;
    }
  }

  openModal(user: User): void{
    this.dialog.open(SayThankModalComponent, {
      width: '40%',
      panelClass: 'custom-modalbox',
      data: user
    });
  }
}
