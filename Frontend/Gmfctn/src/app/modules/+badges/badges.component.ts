import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-badges',
  templateUrl: './badges.component.html',
  styleUrls: ['./badges.component.scss']
})
export class BadgesComponent implements OnInit, OnDestroy {
  subscription = new Subscription();
  height = 0;
  margin = '';
  user!: User;

  constructor(private profileService: ProfileService) {}

  ngOnInit(): void {
    this.calculateSize();
    this.subscription.add(this.profileService.currentUser$.subscribe( user => this.user = user));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  calculateSize(): void {
    const toolbarHeight = 75;
    const rowsCount = 2;
    const offsetForMargin = 10;

    this.height = ( document.body.clientHeight - toolbarHeight ) / rowsCount - 2 * offsetForMargin;
    this.margin = document.body.clientHeight / 100 + offsetForMargin + '';
  }
}
