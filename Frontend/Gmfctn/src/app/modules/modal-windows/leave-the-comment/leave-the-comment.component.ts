import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-leave-the-comment',
  templateUrl: './leave-the-comment.component.html',
  styleUrls: ['./leave-the-comment.component.scss']
})
export class LeaveTheCommentComponent implements OnInit {
  userForm: FormGroup = new FormGroup({});
  message = '';
  constructor(private readonly fb: FormBuilder, private dialogRef: MatDialogRef<LeaveTheCommentComponent>) { }

  ngOnInit(): void {
    this.userForm = this.fb.group({
      message: this.fb.control(this.message, Validators.required),
    });
  }

  submit(): void {
    if (this.userForm.valid) {
        this.message = this.userForm.value;
        this.close();
    }
  }

  close(): void {
    this.dialogRef.close();
  }
}
