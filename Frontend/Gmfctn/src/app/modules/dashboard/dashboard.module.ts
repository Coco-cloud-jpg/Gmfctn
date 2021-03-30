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
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MaterialModule,
  ],
})
export class DashboardModule { }
