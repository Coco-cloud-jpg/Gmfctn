import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { RouterModule } from '@angular/router';
import { GreetingComponent } from './greeting/greeting.component';
import { MaterialModule } from './../../shared/materials.module';

import { TopChartComponent } from './top-chart/top-chart.component';
import { BadgesComponent } from './badges/badges.component';
import { ChallengesComponent } from './challenges/challenges.component';
import { ExoftAchievementsComponent } from './exoft-achievements/exoft-achievements.component';
import {ReactiveFormsModule} from '@angular/forms';
import { ModalWindowModule } from '../modal-windows/modal-window.module';
import { SharedComponentsModuleModule } from '../shared-components-module/shared-components-module.module';

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
    MaterialModule,
    ModalWindowModule,
    SharedComponentsModuleModule
  ],
})
export class DashboardModule { }
