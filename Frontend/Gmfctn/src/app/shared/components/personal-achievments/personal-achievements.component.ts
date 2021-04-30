import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import '../../models/achievement';
import { MatDialog } from '@angular/material/dialog';
import { RequestModalComponent } from '../request-modal/request-modal.component';
import { Achievement } from '../../models/achievement';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-personal-achievements',
  templateUrl: './personal-achievements.component.html',
  styleUrls: ['./personal-achievements.component.scss'],
})
export class PersonalAchievmentsComponent implements OnInit, OnDestroy {
  @Input() containFourAchievements = false;
  headText = 'Last achievements';
  subscription = new Subscription();
  notFound = false;
  achList!: Achievement[];

  constructor(public dialog: MatDialog, private profileService: ProfileService) {}

  ngOnInit(): void {
    if (!this.profileService.currentUser$.value.achievements) {
    this.subscription.add(this.profileService.currentUser$.subscribe( user => {
      this.achList = user.achievements ?? [];

      if (this.achList.length === 0) {
        this.notFound = true;

        return;
      }

      this.achList.sort( (a, b) => b.time.getTime() - a.time.getTime());
      this.achList = this.achList.slice(0, 5);

      if (this.containFourAchievements) {
        this.dislayFourAchievements();
        this.headText = 'Personal achievements';
      }
    }
    ));
    }
    else {
      this.subscription.add(this.profileService.currentUser$.subscribe( user => {
        this.achList = user.achievements ?? [];

        if (this.achList.length === 0) {
          this.notFound = true;

          return;
        }

        this.achList.sort( (a, b) => b.time.getTime() - a.time.getTime());
        this.achList = this.achList.slice(0, 5);

        if (this.containFourAchievements) {
          this.dislayFourAchievements();
          this.headText = 'Personal achievements';
        }
      }
      ));
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  dislayFourAchievements(): void {
    if (this.achList?.length > 4) {
      this.achList?.pop();
    }
  }

  openModal(): void {
    this.dialog.open(RequestModalComponent, {
      width: '40%',
      panelClass: 'custom-modalbox',
    });
  }
}
