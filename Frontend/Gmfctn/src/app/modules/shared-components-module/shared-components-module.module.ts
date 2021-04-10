import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonalAchievmentsComponent } from './personal-achievments/personal-achievements.component';
import { PassedTimePipePipe } from '../../pipes/passed-time-pipe.pipe';
import { MaterialModule} from '../../shared/materials.module';
import { ThankYouComponent } from './thank-you/thank-you.component';
@NgModule({
  declarations: [
    PersonalAchievmentsComponent,
    PassedTimePipePipe,
    ThankYouComponent
  ],
  imports: [
    CommonModule,
    MaterialModule
  ],
  exports: [
    PersonalAchievmentsComponent,
    PassedTimePipePipe,
    ThankYouComponent
  ]
})
export class SharedComponentsModuleModule { }
