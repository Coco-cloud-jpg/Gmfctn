import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LeaveTheCommentComponent } from '../leave-the-comment/leave-the-comment.component';
import { Achievement } from '../../models/achievement';
import { User } from '../../models/user';
import { AchievementServiceService } from 'src/app/core/services/achievement-service/achievement-service.service';
import { UserSI } from '../../models/user-short-info';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-say-thank-modal',
  templateUrl: './say-thank-modal.component.html',
  styleUrls: ['./say-thank-modal.component.scss'],
})
export class SayThankModalComponent implements OnInit, OnDestroy {
  achList: Achievement[] = [
  ];

  notFound = false;
  badgesQuantity: [Achievement, number][] = [];
  subscription = new Subscription();

  constructor(
    public dialog: MatDialog,
    private dialogRef: MatDialogRef<SayThankModalComponent>,
    @Inject(MAT_DIALOG_DATA) public user: UserSI,
    private achivementService: AchievementServiceService
  ) {}

  ngOnInit(): void {
    this.subscription.add(this.achivementService.getUserAchievementsById(this.user.id)
    .subscribe(achievements => {
      this.achList = achievements;
      this.calculateBadges();
      if ( achievements.length === 0) {
        this.notFound = true;
      }
    }));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  close(): void {
    this.dialogRef.close();
  }

  private calculateBadges(): void {
    this.achList.forEach((element) => {
      if ( this.badgesQuantity.find((el) => el[0].name === element.name) === undefined ) {
        this.badgesQuantity.push([element, 1]);
      } else {
        this.badgesQuantity[ this.badgesQuantity.findIndex((el) => el[0].name === element.name) ][1]++;
      }
    });
  }

  openModal(): void {
    this.dialog.open(LeaveTheCommentComponent, {
      width: '21%',
      panelClass: 'custom-modalbox',
      data: this.user.id
    });
  }
}
