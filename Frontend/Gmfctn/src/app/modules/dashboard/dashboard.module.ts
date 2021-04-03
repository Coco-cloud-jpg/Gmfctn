import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { RouterModule } from '@angular/router';
import { GreetingComponent } from './greeting/greeting.component';
import { MaterialModule } from './../../shared/materials.module';
import { PersonalAchievmentsComponent } from './personal-achievments/personal-achievements.component';

import { PassedTimePipePipe } from '../../pipes/passed-time-pipe.pipe';
import { ThankYouComponent } from './thank-you/thank-you.component';
import { TopChartComponent } from './top-chart/top-chart.component';
import { BadgesComponent } from './badges/badges.component';
import { ChallengesComponent } from './challenges/challenges.component';
import { ExoftAchievementsComponent } from './exoft-achievements/exoft-achievements.component';
import {ReactiveFormsModule} from '@angular/forms';
import {ModalWindowModule} from '../modal-windows/modal-window.module';

const routes = [
  {
      path: '',
      component: DashboardComponent,
  }
];

@NgModule({
  declarations: [
    GreetingComponent,
    DashboardComponent,
    PersonalAchievmentsComponent,
    PassedTimePipePipe,
    ThankYouComponent,
    TopChartComponent,
    BadgesComponent,
    ChallengesComponent,
    ExoftAchievementsComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MaterialModule,
    ModalWindowModule,
  ],
})
export class DashboardModule { }
