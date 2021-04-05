import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SayThankModalComponent } from '../../modal-windows/say-thank-modal/say-thank-modal.component';
import '../../../models/user';

@Component({
  selector: 'app-thank-you',
  templateUrl: './thank-you.component.html',
  styleUrls: ['./thank-you.component.scss']
})
export class ThankYouComponent{

  user: User = {
    name: 'Petro',
    surname: 'Poroshenko',
    total: 100,
    icon: '../../../../assets/5.jpg'
  };

  constructor(public dialog: MatDialog) {}

  openModal(): void{
    this.dialog.open(SayThankModalComponent, {
      width: '50%',
      panelClass: 'custom-modalbox',
      data: this.user
    });
  }
}
