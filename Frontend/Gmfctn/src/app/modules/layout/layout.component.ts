import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
  providers: [ProfileService]
})
export class LayoutComponent implements OnInit, OnDestroy{
  subscribtion: Subscription = new Subscription();
  user!: User;

  constructor(private profileService: ProfileService) { }

  ngOnInit(): void {
    this.subscribtion.add(this.profileService
          .getUserInfo().pipe(take(1)).subscribe(user => {
              this.user = user;
          }));
    this.profileService.currentUser$.subscribe(user => {
      this.user = user;
    });
  }

  ngOnDestroy(): void {
    this.subscribtion.unsubscribe();
  }
}
