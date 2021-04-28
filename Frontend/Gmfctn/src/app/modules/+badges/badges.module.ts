import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

import { BadgesComponent } from './badges.component';
import { TotalAchievementsXpComponent } from './components/total-achievements-xp/total-achievements-xp.component';
import { UserBlockComponent } from './components/user-block/user-block.component';
import { FullListOfAchievementsComponent } from './components/full-list-of-achievements/full-list-of-achievements.component';

const routes = [
  {
      path: '',
      component: BadgesComponent,
  }
];

@NgModule({
  declarations: [
    BadgesComponent,
    TotalAchievementsXpComponent,
    UserBlockComponent,
    FullListOfAchievementsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class BadgesModule { }
