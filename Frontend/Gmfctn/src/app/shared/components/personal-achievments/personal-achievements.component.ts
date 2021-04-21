import { Component, Input, OnInit } from '@angular/core';
import '../../models/achievement';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { RequestModalComponent } from '../request-modal/request-modal.component';
import { Achievement } from '../../models/achievement';

@Component({
  selector: 'app-personal-achievements',
  templateUrl: './personal-achievements.component.html',
  styleUrls: ['./personal-achievements.component.scss'],
})
export class PersonalAchievmentsComponent implements OnInit {
  @Input() containFourAchievements = false;
  headText = 'Last achievements';

  achList: Achievement[] = [
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft turbo power ',
      description: '',
      xp: 5,
      time: new Date('December 17, 2005 03:24:00'),
    },
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft turbo power',
      description: '',
      xp: 5,
      time: new Date(),
    },
    {
      icon: '../../../../assets/phoenix.png',
      name: 'Exoft skylark power',
      description: '',
      xp: 5,
      time: new Date('March 27, 2021 20:24:00'),
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
  ];

  constructor(public dialog: MatDialog) {}

  ngOnInit(): void {
    if (this.containFourAchievements) {
      this.dislayFourAchievements();
      this.headText = 'Personal achievements';
    }

    this.achList.sort((a, b) => b.time.getTime() - a.time.getTime());
  }

  dislayFourAchievements(): void {
    this.achList.pop();
  }

  openModal(): void {
    this.dialog.open(RequestModalComponent, {
      width: '40%',
      panelClass: 'custom-modalbox',
    });
  }
}
