import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/core/services/users-service/user.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit, OnDestroy {
  passForm: FormGroup = new FormGroup({});
  passwords = {oldPassword : '' , newPassword : '', confirmedPassword: ''};
  private readonly passwordRegex: RegExp = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,32}$/;
  subscription = new Subscription();

  constructor(
    private readonly fb: FormBuilder,
    private dialogRef: MatDialogRef<ChangePasswordComponent>,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.passForm = this.fb.group({
      oldPassword: this.fb.control(this.passwords.oldPassword, Validators.required),
      newPassword: this.fb.control(this.passwords.newPassword, this.passwordValidator.bind(this)),
      confpassword: this.fb.control(this.passwords.confirmedPassword, this.isEqual.bind(this))
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  passwordValidator(control: AbstractControl): ValidationErrors | null {
    if (control?.value) {
        const isValid = this.passwordRegex.test(control.value);

        return !isValid ? {invalidPassword: true} : null;
    }

    return {invalidPassword: true};
  }

  isEqual(control: AbstractControl): ValidationErrors | null {
    if (control?.value) {
      return control.value !== this.passForm.value.newPassword ? {invalidPassword: true} : null;
    }

    return {invalidPassword: true};
  }

  changePassword(): void {
    if (this.passForm.valid) {
      this.passwords = this.passForm.value;
      const data = {oldPassword : this.passwords.oldPassword , newPassword : this.passwords.newPassword};
      this.subscription.add(this.userService.passwordChange(data).subscribe());
    }
  }

  close(): void {
    this.dialogRef.close();
  }

}
