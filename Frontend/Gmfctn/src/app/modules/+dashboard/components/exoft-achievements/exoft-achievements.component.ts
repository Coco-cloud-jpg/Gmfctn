import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SayThankModalComponent } from '../../../../shared/components/say-thank-modal/say-thank-modal.component';

@Component({
  selector: 'app-exoft-achievements',
  templateUrl: './exoft-achievements.component.html',
  styleUrls: ['./exoft-achievements.component.scss'],
})
export class ExoftAchievementsComponent implements OnInit {
  achList = [
    {
      name: 'Pepe',
      surname: 'Jodgi',
      icon: '../../../../assets/phoenix.png',
      eventText: 'Exoft turbo power',
      time: new Date('March 17, 2021 03:24:00'),
    },
    {
      name: 'Petro',
      surname: 'Poroshenko',
      icon: '../../../../assets/5.jpg',
      eventText: 'Exoft party power',
      time: new Date('April 2, 2021 03:24:00'),
    },
    {
      name: 'Shrek',
      surname: 'Bolothnyi',
      icon: '',
      eventText: 'Exoft corporate power',
      time: new Date('March 27, 2021 03:24:00'),
    },
    {
      name: 'Mr.',
      surname: 'Heisenberg',
      icon: '',
      eventText: 'Exoft skylark power',
      time: new Date('March 17, 2021 03:24:00'),
    },
    {
      name: 'Ihor',
      surname: 'Da',
      icon: '',
      eventText: 'Exoft turbo power',
      time: new Date('December 17, 2005 03:24:00'),
    },
  ];

  constructor(public dialog: MatDialog) {}

  ngOnInit(): void {
    this.fillData();
  }

  private fillData(): void {
    this.achList.sort((a, b) => b.time.getTime() - a.time.getTime());
  }

  openModal(user: object): void {
    this.dialog.open(SayThankModalComponent, {
      width: '40%',
      panelClass: 'custom-modalbox',
      data: user,
    });
  }
}
