import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProfileSettingModalComponent } from '../modal-windows/profile-setting-modal/profile-setting-modal.component';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent{

  @Input() user!: User;
  opened = false;

  constructor(private dialog: MatDialog) {}

  editProfile(): void {
    this.dialog.open(ProfileSettingModalComponent, {
      width: '30%',
      panelClass: 'custom-modalbox',
      data: this.user
    });
  }
}
