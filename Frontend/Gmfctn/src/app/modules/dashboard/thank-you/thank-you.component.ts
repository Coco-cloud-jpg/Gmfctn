import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SaythankModalComponent } from '../../modal-windows/saythank-modal/saythank-modal.component';
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

  public openModal(): void{
    const dialogConfig = this.dialog.open(SaythankModalComponent, {
      width: '50%',
      panelClass: 'custom-modalbox',
      data: this.user
    });
  }
}
