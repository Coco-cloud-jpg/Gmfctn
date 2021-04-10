import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-profile-setting-modal',
  templateUrl: './profile-setting-modal.component.html',
  styleUrls: ['./profile-setting-modal.component.scss'],
})
export class ProfileSettingModalComponent implements OnInit {
  userForm: FormGroup = new FormGroup({});

  constructor(
    private readonly fb: FormBuilder,
    private dialogRef: MatDialogRef<ProfileSettingModalComponent>,
    @Inject(MAT_DIALOG_DATA) public user: User
  ) {}

  ngOnInit(): void {
    this.userForm = this.fb.group({
      firstName: this.fb.control(this.user.name, Validators.required),
      lastName: this.fb.control(this.user.surname, Validators.required),
      email: this.fb.control(this.user.email, [
        Validators.required,
        Validators.email,
      ]),
      status: this.fb.control(this.user.status),
    });
  }

  updateUser(): void {
    if (this.userForm.valid) {
      this.user = { ...this.userForm.value };
      this.close();
    }
  }

  close(): void {
    this.dialogRef.close();
  }
}
