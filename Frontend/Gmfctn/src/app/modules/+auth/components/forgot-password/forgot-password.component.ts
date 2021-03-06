import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { SuccesfullyResetComponent } from 'src/app/shared/components/succesfully-reset/succesfully-reset.component';
import { ResetPasswordService } from '../../services/reset-password/reset-password.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  email = '';
  resetPasswordForm = new FormGroup({});
  emailForm = new FormGroup({});
  dataToReset = {newPassword: '', hash: ''};
  errorOccured = false;
  isMessageSent = false;
  emailRegexp = /^[^\s@]+@[^\s@]+$/;
  isValid = false;

  constructor(private readonly fb: FormBuilder, private resetPasswordService: ResetPasswordService,
              private router: Router, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.emailForm = this.fb.group({
      email: this.fb.control(this.email, [
        Validators.required,
        Validators.pattern(this.emailRegexp),
      ])
    });
    this.resetPasswordForm = this.fb.group({
      hash: this.fb.control(this.dataToReset.hash, Validators.required),
      newPassword: this.fb.control(this.dataToReset.newPassword,
        [
          Validators.required,
          Validators.pattern(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,32}$/)
        ]),
    });
  }

  sendRequest(): void {
    this.email = this.emailForm.value.email;
    this.resetPasswordService.sendRequest(this.email).pipe(take(1)).subscribe(res => {
       this.errorOccured = res === 'error';
       this.isMessageSent = res !== 'error';
    });
  }

  resetPassword(): void {
    if (this.resetPasswordForm.valid) {
      this.dataToReset = this.resetPasswordForm.value;
      this.resetPasswordService.resetPassword(this.dataToReset).pipe(take(1)).subscribe(res => {
        this.errorOccured = res === 'error';
        if (res !== 'error') {
          this.snackBar.openFromComponent(SuccesfullyResetComponent, {
            duration: 5000,
          });
          this.router.navigate(['/auth/sign-in']);
        }
      });
    }
  }
}
