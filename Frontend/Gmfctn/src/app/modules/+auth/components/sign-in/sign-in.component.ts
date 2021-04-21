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
  errorMessage = '';
  loading = false;
  subscription$ = new Subscription();
  initialFormData = new FormGroup({});
  user = {userName: '', password: ''};


  constructor(private readonly fb: FormBuilder, private authenticateService: AuthenticateService, private router: Router) { }

  ngOnInit(): void {
    this.subscription$.add(this.authenticateService
                                      .isErrorInAuthorization$
                                      .subscribe( val => this.errorMessage = val ? 'Username or password are incorrect' : '' ));

    this.initialFormData = this.fb.group({
      userName: this.fb.control(this.user.userName, Validators.required),
      password: this.fb.control(this.user.password, Validators.required),
    });
  }

  ngOnDestroy(): void {
    this.subscription$.unsubscribe();
  }

  signIn(): void {
      this.loading = true;
      this.subscription$.add(this.authenticateService
        .authenticate(this.initialFormData.value.userName, this.initialFormData.value.password)
        .pipe(take(1), finalize(() => this.loading = false)).subscribe(() => {
            this.router.navigate(['/home']);
      }));
      this.loading = false;
  }
}
