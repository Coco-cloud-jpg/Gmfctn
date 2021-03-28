import { Component, OnInit } from '@angular/core';
import { DashboardModule } from '../dashboard.module'
import '../../../models/achievement'
@Component({
  selector: 'personal-achievments',
  templateUrl: './personal-achievments.component.html',
  styleUrls: ['./personal-achievments.component.scss']
})
export class PersonalAchievmentsComponent implements OnInit {

  achList:Achievement[] = [{icon: '../../../../assets/phoenix.png',
                            name: 'Exoft turbo power',
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
                            time: new Date('March 27, 2021 22:24:00')}];
  constructor() { }

  ngOnInit(): void {
  }

}
