import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Achievement } from '../../models/achievement';
import { RequestForAchievement } from '../../models/request';

@Component({
  selector: 'app-request-modal',
  templateUrl: './request-modal.component.html',
  styleUrls: ['./request-modal.component.scss']
})
export class RequestModalComponent implements OnInit {

  request: RequestForAchievement = {
    achievement: '',
    theme: ''
  };
  userForm: FormGroup = new FormGroup({});

  achList: Achievement[] = [{icon: '../../../../assets/phoenix.png',
                              name: 'Exoft turbo power',
                              description: '',
                              xp: 5,
                              time: new Date('December 17, 2005 03:24:00')},
                              {icon: '../../../../assets/phoenix.png',
                              name: 'Exoft skylark power',
                              description: '',
                              xp: 5,
                              time: new Date('March 27, 2021 20:24:00')},
                              {icon: '../../../../assets/phoenix.png',
                              name: 'Exoft corporate power',
                              description: '',
                              xp: 5,
                              time: new Date('March 27, 2021 22:24:00')}];

  constructor(private readonly fb: FormBuilder, private dialogRef: MatDialogRef<RequestModalComponent>) {
  }

  ngOnInit(): void {
    this.userForm = this.fb.group({
      achievement: this.fb.control(this.request.achievement, Validators.required),
      theme: this.fb.control(this.request.theme, Validators.required),
    });
  }

  submit(): void {
    if (this.userForm.valid) {
        this.request = this.userForm.value;
        this.close();
    }
  }

  close(): void {
    this.dialogRef.close();
  }
}
