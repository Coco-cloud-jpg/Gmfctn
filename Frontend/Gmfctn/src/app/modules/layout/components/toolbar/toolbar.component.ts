import { Component, Output, EventEmitter, Input} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { User } from 'src/app/shared/models/user';
import { defaultUser } from 'src/app/shared/models/dafault-user';
import { ProfileSettingModalComponent } from '../../../../shared/components/profile-setting-modal/profile-setting-modal.component';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {

  @Input() isOpened = false;
  @Input() user: User = defaultUser;

  @Output() opened = new EventEmitter<boolean>();

  constructor(private dialog: MatDialog) { }

  editProfile(): void {
    this.dialog.open(ProfileSettingModalComponent, {
      width: '30%',
      panelClass: 'custom-modalbox',
      data: this.user
    });
  }

  open(): void {
    this.opened.emit(!this.isOpened);
  }
}
