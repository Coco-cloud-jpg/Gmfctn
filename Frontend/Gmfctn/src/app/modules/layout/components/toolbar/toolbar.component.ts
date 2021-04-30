import { Component, Output, EventEmitter, Input, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { User } from 'src/app/shared/models/user';
import { ProfileSettingModalComponent } from '../../../../shared/components/profile-setting-modal/profile-setting-modal.component';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';
import { Router } from '@angular/router';
import { ChangePasswordComponent } from 'src/app/shared/components/change-password/change-password.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnDestroy {

  @Input() isOpened = false;
  @Input()user!: User;

  @Output() opened = new EventEmitter<boolean>();

  subscription = new Subscription();

  constructor(private dialog: MatDialog, private router: Router, private profileService: ProfileService) { }

  editProfile(): void {
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

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  changePassword(): void {
    this.dialog.open(ChangePasswordComponent, {
      width: '30%',
      panelClass: 'custom-modalbox',
    });
  }

  open(): void {
    this.opened.emit(!this.isOpened);
  }

  loggout(): void {
    localStorage.clear();
    this.router.navigate(['/auth/sign-in']);
  }
}
