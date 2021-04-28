import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

import { GreetingComponent } from './components/greeting/greeting.component';
import { TopChartComponent } from './components/top-chart/top-chart.component';
import { BadgesComponent } from './components/badges/badges.component';
import { ChallengesComponent } from './components/challenges/challenges.component';
import { ExoftAchievementsComponent } from './components/exoft-achievements/exoft-achievements.component';


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
    TopChartComponent,
    BadgesComponent,
    ChallengesComponent,
    ExoftAchievementsComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
  ],
})
export class DashboardModule { }
