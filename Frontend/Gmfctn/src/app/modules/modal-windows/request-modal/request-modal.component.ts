import {Component, NgModule, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import '../../../models/request';
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
  userForm!: FormGroup;



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
        console.log(this.request);
        this.close();

    }
  }

  close(): void {

    this.dialogRef.close();
  }
}
