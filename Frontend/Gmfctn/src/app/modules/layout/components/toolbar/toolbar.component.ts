import { Component, Output, EventEmitter, Input, OnInit, OnDestroy} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { User } from 'src/app/shared/models/user';
import { defaultUser } from 'src/app/shared/models/dafault-user';
import { ProfileSettingModalComponent } from '../../../../shared/components/profile-setting-modal/profile-setting-modal.component';
import { Subscription } from 'rxjs';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit, OnDestroy {

  @Input() isOpened = false;
  user!: User;

  @Output() opened = new EventEmitter<boolean>();

  subscription$ = new Subscription();

  constructor(private dialog: MatDialog, private profileService: ProfileService) { }

  ngOnInit(): void {
    this.subscription$.add(this.profileService.currentUser$.subscribe(user => this.user = user));
  }

  ngOnDestroy(): void {
    this.subscription$.unsubscribe();
  }

  editProfile(): void {
    const dialogRef =this.dialog.open(ProfileSettingModalComponent, {
      width: '30%',
      panelClass: 'custom-modalbox',
      data: this.user
    });
    dialogRef.afterClosed().subscribe(user => {
      if (JSON.stringify(user) !== JSON.stringify(this.user) ) {
        this.profileService.currentUser$.next(user);
      }
    });
  }

  open(): void {
    this.opened.emit(!this.isOpened);
  }
}
