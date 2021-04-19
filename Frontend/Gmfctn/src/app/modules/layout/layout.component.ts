import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { ProfileService } from 'src/app/core/services/profile-service/profile.service';
import { Roles, User } from 'src/app/shared/models/user';
import { defaultUser } from 'src/app/shared/models/dafault-user';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
  providers: [ProfileService]
})
export class LayoutComponent implements OnInit, OnDestroy{
  isSignedIn = false;
  subscribtion: Subscription = new Subscription;
  user: User =  defaultUser;

  constructor(private profileService: ProfileService) { }

  ngOnInit(): void {
    this.subscribtion = this.profileService
          .getUserInfo().pipe(take(1)).subscribe(user => {
              this.user = user;
          });
  }

  ngOnDestroy(): void {
    this.subscribtion.unsubscribe();
  }
}
