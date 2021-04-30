import { Component, Input, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProfileSettingModalComponent } from '../../../../shared/components/profile-setting-modal/profile-setting-modal.component';
import { User } from 'src/app/shared/models/user';
import { defaultUser } from 'src/app/shared/models/dafault-user';
import { Subscription } from 'rxjs';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';
import { Router } from '@angular/router';
import { ChangePasswordComponent } from 'src/app/shared/components/change-password/change-password.component';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnDestroy {
  @Input()user!: User;
  opened = false;

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

  loggout(): void {
    localStorage.clear();
    this.router.navigate(['/auth/sign-in']);
  }
}
