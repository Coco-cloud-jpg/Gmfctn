import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { finalize, take } from 'rxjs/operators';
import { AuthenticateService } from '../../services/authenticate.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent implements OnInit, OnDestroy {
  @Output() isSigned = new EventEmitter<boolean>();
  state = '';
  loading = false;
  subscribtion$: Subscription = new Subscription;
  subscribtionForCorrectness$: Subscription = new Subscription;
  userForm: FormGroup = new FormGroup({});
  user: {userName: string, password: string} = {userName: '', password: ''};


  constructor(private readonly fb: FormBuilder, private authenticateService: AuthenticateService, private router: Router) { }

  ngOnInit(): void {
    this.subscribtionForCorrectness$ = this.authenticateService
                                      .isErrorInAuthorization$
                                      .subscribe( val => this.state = val ? 'Username or password are incorrect' : '' );

    this.userForm = this.fb.group({
      userName: this.fb.control(this.user.userName, Validators.required),
      password: this.fb.control(this.user.password, Validators.required),
    });
  }

  ngOnDestroy(): void {
    this.subscribtion$.unsubscribe();
    this.subscribtionForCorrectness$.unsubscribe();
  }

  signIn(): void {
      this.loading = true;
      this.subscribtion$ = this.authenticateService
      .authenticate(this.userForm.value.userName, this.userForm.value.password)
      .pipe(take(1), finalize(() => this.loading = false)).subscribe(() => {
          this.router.navigate(['/home']);
      });
  }
}
