import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SayThankModalComponent } from '../say-thank-modal/say-thank-modal.component';
import { Roles, User } from '../../models/user';

@Component({
  selector: 'app-thank-you',
  templateUrl: './thank-you.component.html',
  styleUrls: ['./thank-you.component.scss']
})
export class ThankYouComponent{

  user: User = {
    firstName: 'Petro',
    lastName: 'Poroshenko',
    userName: 'Petya',
    xp: 100,
    avatarId: '../../../../assets/5.jpg',
    status: '',
    email: 'asdqweqw@gmail.com',
    id: 'asdqwe',
    roles: [Roles.User]
  };

  constructor(public dialog: MatDialog) {}

  openModal(): void {
    this.dialog.open(SayThankModalComponent, {
      width: '50%',
      panelClass: 'custom-modalbox',
      data: this.user
    });
  }
}
