import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RequestModalComponent} from './request-modal/request-modal.component';
import {RouterModule} from '@angular/router';
import {MaterialModule} from '../../shared/materials.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { SayThankModalComponent } from './say-thank-modal/say-thank-modal.component';
import { LeaveTheCommentComponent } from './leave-the-comment/leave-the-comment.component';


@NgModule({
    declarations: [RequestModalComponent, SayThankModalComponent, LeaveTheCommentComponent],
    imports: [
        CommonModule, FormsModule, ReactiveFormsModule, MaterialModule
    ],
    exports: [
        RequestModalComponent, SayThankModalComponent
    ]
})
export class ModalWindowModule {
}
