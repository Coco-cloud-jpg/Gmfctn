import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProfileSettingModalComponent } from '../../modal-windows/profile-setting-modal/profile-setting-modal.component';

@Component({
  selector: 'app-user-block',
  templateUrl: './user-block.component.html',
  styleUrls: ['./user-block.component.scss']
})
export class UserBlockComponent {
  public user: User = {name: 'Aezakmi', surname: 'Houston', total: 0, icon: '../../../../assets/1.jpg', status: '', email: 'asdqweqw@gmail.com'};

  constructor(private dialog: MatDialog) { }

  openModal(): void {
    this.dialog.open(ProfileSettingModalComponent, {
      width: '30%',
      panelClass: 'custom-modalbox',
      data: this.user
    });
  }
}
