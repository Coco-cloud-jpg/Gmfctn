import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignInWindowComponent } from './sign-in-window.component';

describe('SignInWindowComponent', () => {
  let component: SignInWindowComponent;
  let fixture: ComponentFixture<SignInWindowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SignInWindowComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SignInWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
