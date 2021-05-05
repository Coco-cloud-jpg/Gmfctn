import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './modules/materials.module';
import { MatFormFieldModule } from '@angular/material/form-field';

import { LeaveTheCommentComponent } from './components/leave-the-comment/leave-the-comment.component';
import { SayThankModalComponent } from './components/say-thank-modal/say-thank-modal.component';
import { ProfileSettingModalComponent } from './components/profile-setting-modal/profile-setting-modal.component';
import { RequestModalComponent } from './components/request-modal/request-modal.component';
import { ThankYouComponent } from './components/thank-you/thank-you.component';
import { PersonalAchievmentsComponent } from './components/personal-achievments/personal-achievements.component';

import { PassedTimePipePipe } from './pipes/passed-time-pipe/passed-time-pipe.pipe';
import { SuccesfullyResetComponent } from './components/succesfully-reset/succesfully-reset.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { NotFoundComponent } from './components/not-found/not-found.component';


@NgModule({
  declarations: [
    LeaveTheCommentComponent,
    SayThankModalComponent,
    ThankYouComponent,
    ProfileSettingModalComponent,
    RequestModalComponent,
    PersonalAchievmentsComponent,
    PassedTimePipePipe,
    SuccesfullyResetComponent,
    ChangePasswordComponent,
    NotFoundComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    MatFormFieldModule
  ],
  exports: [
    LeaveTheCommentComponent,
    SayThankModalComponent,
    ThankYouComponent,
    ProfileSettingModalComponent,
    RequestModalComponent,
    PersonalAchievmentsComponent,
    PassedTimePipePipe,
    MaterialModule
  ]
})
export class SharedModule { }
