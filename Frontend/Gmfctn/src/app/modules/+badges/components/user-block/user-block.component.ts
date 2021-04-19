import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { ProfileSettingModalComponent } from '../../../../shared/components/profile-setting-modal/profile-setting-modal.component';

import { Roles, User } from 'src/app/shared/models/user';
import { defaultUser } from 'src/app/shared/models/dafault-user';

@Component({
  selector: 'app-user-block',
  templateUrl: './user-block.component.html',
  styleUrls: ['./user-block.component.scss']
})
export class UserBlockComponent {
  @Input()
  user: User = defaultUser;

  constructor(private dialog: MatDialog) { }

  openModal(): void {
    this.dialog.open(ProfileSettingModalComponent, {
      width: '30%',
      panelClass: 'custom-modalbox',
      data: this.user
    });
  }
}
