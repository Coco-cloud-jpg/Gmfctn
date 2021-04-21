import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProfileSettingModalComponent } from '../../../../shared/components/profile-setting-modal/profile-setting-modal.component';
import { User } from 'src/app/shared/models/user';
import { defaultUser } from 'src/app/shared/models/dafault-user';
import { Subscription } from 'rxjs';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit, OnDestroy {

  user!: User;
  opened = false;

  subscription$ = new Subscription();

  constructor(private dialog: MatDialog, private profileService: ProfileService) { }

  ngOnInit(): void {
    this.subscription$.add(this.profileService.currentUser$.subscribe(user => this.user = user));
  }

  ngOnDestroy(): void {
    this.subscription$.unsubscribe();
  }

  editProfile(): void {
    const dialogRef = this.dialog.open(ProfileSettingModalComponent, {
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
}
