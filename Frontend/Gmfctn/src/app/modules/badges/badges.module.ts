import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BadgesComponent } from './badges.component';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/shared/materials.module';
import { ModalWindowModule } from '../modal-windows/modal-window.module';
import {SharedComponentsModuleModule } from '../shared-components-module/shared-components-module.module';
import { TotalAchievementsXpComponent } from './total-achievements-xp/total-achievements-xp.component';
import { UserBlockComponent } from './user-block/user-block.component';
import { FullListOfAchievementsComponent } from './full-list-of-achievements/full-list-of-achievements.component';

const routes = [
  {
      path: '',
      component: BadgesComponent,
  }
];

@NgModule({
  declarations: [BadgesComponent, TotalAchievementsXpComponent, UserBlockComponent, FullListOfAchievementsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MaterialModule,
    ModalWindowModule,
    SharedComponentsModuleModule
  ]
})
export class BadgesModule { }
