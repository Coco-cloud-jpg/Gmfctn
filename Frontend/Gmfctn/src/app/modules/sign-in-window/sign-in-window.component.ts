import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-in-window',
  templateUrl: './sign-in-window.component.html',
  styleUrls: ['./sign-in-window.component.scss']
})
export class SignInWindowComponent implements OnInit {
  @Output() isSigned = new EventEmitter<boolean>();

  userForm: FormGroup = new FormGroup({});
  user: {userName: string, password: string} = {userName: '', password: ''};

  constructor(private readonly fb: FormBuilder) {}

  ngOnInit(): void {
    this.userForm = this.fb.group({
      userName: this.fb.control(this.user.userName, Validators.required),
      password: this.fb.control(this.user.password, Validators.required),
    });
  }

  signIn(): void {
    if (this.userForm.valid) {
      this.isSigned.emit(true);
    }
  }
}
