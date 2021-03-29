import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { RouterModule } from '@angular/router';
import { GreetingComponent } from './greeting/greeting.component';
import {MaterialModule} from './../../shared/materials.module';
import { PersonalAchievmentsComponent } from './personal-achievments/personal-achievments.component';
import { HowLongAgoPipe } from './personal-achievments/how-long-ago.pipe';
import { ThankYouComponent } from './thank-you/thank-you.component';
import { TopChartComponent } from './top-chart/top-chart.component';


const routes = [
  {
      path: '',
      component: DashboardComponent,
  }
];

@NgModule({
  declarations: [GreetingComponent, DashboardComponent, PersonalAchievmentsComponent, HowLongAgoPipe, ThankYouComponent, TopChartComponent],
  imports: [
    CommonModule, RouterModule.forChild(routes), MaterialModule
  ],
})
export class DashboardModule { }
