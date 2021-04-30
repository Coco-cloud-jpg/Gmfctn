import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { ProfileSettingModalComponent } from '../../../../shared/components/profile-setting-modal/profile-setting-modal.component';

import { User } from 'src/app/shared/models/user';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user-block',
  templateUrl: './user-block.component.html',
  styleUrls: ['./user-block.component.scss']
})
export class UserBlockComponent implements OnInit, OnDestroy {
  @Input()
  user!: User;
  subscription = new Subscription();

  constructor(private dialog: MatDialog, private profileService: ProfileService) { }

  ngOnInit(): void {
    this.subscription.add(this.profileService.currentUser$.subscribe(user => {
      this.user = user;
    }));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  openModal(): void {
    const dialogRef = this.dialog.open(ProfileSettingModalComponent, {
      width: '30%',
      panelClass: 'custom-modalbox',
      data: this.user
    });

    this.subscription.add(dialogRef.afterClosed().subscribe(
      data => {
        if (!!data && (JSON.stringify(this.user) !== JSON.stringify(data))) {
          this.profileService.currentUser$.next(data);
        }
      }
    ));
  }
}
