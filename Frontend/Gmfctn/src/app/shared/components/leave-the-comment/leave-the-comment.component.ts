import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { ThankService } from 'src/app/core/services/thank-service/thank.service';

@Component({
  selector: 'app-leave-the-comment',
  templateUrl: './leave-the-comment.component.html',
  styleUrls: ['./leave-the-comment.component.scss']
})
export class LeaveTheCommentComponent implements OnInit, OnDestroy {
  userForm: FormGroup = new FormGroup({});
  message = '';
  subscription = new Subscription();

  constructor(private readonly fb: FormBuilder, private dialogRef: MatDialogRef<LeaveTheCommentComponent>,
              @Inject(MAT_DIALOG_DATA) public userId: string, private thankService: ThankService) { }

  ngOnInit(): void {
    this.userForm = this.fb.group({
      message: this.fb.control(this.message, Validators.required),
    });
  }

  submit(): void {
    if (this.userForm.valid) {
        this.message = this.userForm.value.message;
        this.subscription.add(this.thankService.sendThank(this.userId, this.message).subscribe());
        this.close();
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  close(): void {
    this.dialogRef.close();
  }
}
