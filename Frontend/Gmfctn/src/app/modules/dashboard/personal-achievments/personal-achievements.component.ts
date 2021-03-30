import { Component, OnInit } from '@angular/core';
import '../../../models/achievement';
@Component({
  selector: 'app-personal-achievements',
  templateUrl: './personal-achievements.component.html',
  styleUrls: ['./personal-achievements.component.scss']
})
export class PersonalAchievmentsComponent {

  achList: Achievement[] = [{icon: '../../../../assets/phoenix.png',
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

}
