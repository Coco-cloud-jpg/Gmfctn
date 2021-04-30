import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscriber, Subscription } from 'rxjs';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';

import { Achievement } from 'src/app/shared/models/achievement';

@Component({
  selector: 'app-full-list-of-achievements',
  templateUrl: './full-list-of-achievements.component.html',
  styleUrls: ['./full-list-of-achievements.component.scss'],
})
export class FullListOfAchievementsComponent implements OnInit, OnDestroy {
  achList!: Achievement[];
  noContent = false;
  subsription = new Subscription();

  constructor(private profileService: ProfileService) {}

  ngOnInit(): void {
    if (!this.profileService.currentUser$.value) {
      this.subsription.add(this.profileService.getUserInfo().subscribe(user => {
        this.achList = user.achievements ?? [];

        if (this.achList.length === 0) {
          this.noContent = true;

          return;
        }

        this.sortByDate();
      }));
    }
    else {
      this.subsription.add(this.profileService.currentUser$.subscribe(user => {
        this.achList = user.achievements ?? [];
        if (this.achList.length === 0) {
          this.noContent = true;

          return;
        }

        this.sortByDate();
      }));
    }
  }

  ngOnDestroy(): void {
    this.subsription.unsubscribe();
  }

  sortByDate(): void {
    this.achList.sort((a, b) => b.time.getTime() - a.time.getTime());
  }
}
