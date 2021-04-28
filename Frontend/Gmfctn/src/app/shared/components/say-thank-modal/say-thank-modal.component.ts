import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LeaveTheCommentComponent } from '../leave-the-comment/leave-the-comment.component';
import { Achievement } from '../../models/achievement';
import { User } from '../../models/user';

@Component({
  selector: 'app-say-thank-modal',
  templateUrl: './say-thank-modal.component.html',
  styleUrls: ['./say-thank-modal.component.scss'],
})
export class SayThankModalComponent implements OnInit {
  achList: Achievement[] = [
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft turbo power',
      description: '',
      xp: 5,
      time: new Date('December 17, 2005 03:24:00'),
    },
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft skylark power',
      description: '',
      xp: 5,
      time: new Date(),
    },
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft corporate power',
      description: '',
      xp: 5,
      time: new Date('March 27, 2021 22:24:00'),
    },
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft corporate power',
      description: '',
      xp: 5,
      time: new Date('March 27, 2021 22:24:00'),
    },
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft owl power',
      description: '',
      xp: 5,
      time: new Date('March 27, 2021 22:24:00'),
    },
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft mentor power',
      description: '',
      xp: 5,
      time: new Date('March 27, 2021 22:24:00'),
    },
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft party power',
      description: '',
      xp: 5,
      time: new Date('March 27, 2021 22:24:00'),
    },
  ];

  badgesQuantity: [Achievement, number][] = [];

  constructor(
    public dialog: MatDialog,
    private dialogRef: MatDialogRef<SayThankModalComponent>,
    @Inject(MAT_DIALOG_DATA) public user: User
  ) {}

  ngOnInit(): void {
    this.calculateBadges();
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
    });
  }
}
