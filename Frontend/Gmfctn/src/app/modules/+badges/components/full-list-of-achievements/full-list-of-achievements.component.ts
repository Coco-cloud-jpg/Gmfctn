import { Component, OnInit } from '@angular/core';

import { Achievement } from 'src/app/shared/models/achievement';

@Component({
  selector: 'app-full-list-of-achievements',
  templateUrl: './full-list-of-achievements.component.html',
  styleUrls: ['./full-list-of-achievements.component.scss']
})
export class FullListOfAchievementsComponent implements OnInit {
  achList: Achievement[] = [{icon: '../../../../assets/phoenix.png',
                              name: 'Exoft turbo power ',
                              description: '',
                              xp: 5,
                              time: new Date('December 17, 2005 03:24:00')},
                              {icon: '../../../../assets/phoenix.png',
                              name: 'Exoft turbo power',
                              description: '',
                              xp: 5,
                              time: new Date()},
                              {icon: '../../../../assets/phoenix.png',
                              name: 'Exoft skylark power',
                              description: '',
                              xp: 5,
                              time: new Date('March 27, 2021 20:24:00')},
                              {icon: '../../../../assets/phoenix.png',
                              name: 'Exoft corporate power',
                              description: '',
                              xp: 5,
                              time: new Date('March 27, 2021 22:24:00')},
                              {icon: '../../../../assets/phoenix.png',
                              name: 'Exoft corporate power',
                              description: '',
                              xp: 5,
                              time: new Date('March 27, 2021 22:24:00')},
                              {icon: '../../../../assets/phoenix.png',
                              name: 'Exoft corporate power',
                              description: '',
                              xp: 5,
                              time: new Date('March 27, 2021 22:24:00')}
                            ];

  ngOnInit(): void {
    this.sortByDate();
  }

  sortByDate(): void {
    this.achList.sort( (a, b) => b.time.getTime() - a.time.getTime());
  }
}
