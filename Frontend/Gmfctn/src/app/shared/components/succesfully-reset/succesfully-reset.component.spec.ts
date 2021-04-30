import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccesfullyResetComponent } from './succesfully-reset.component';

describe('SuccesfullyResetComponent', () => {
  let component: SuccesfullyResetComponent;
  let fixture: ComponentFixture<SuccesfullyResetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuccesfullyResetComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SuccesfullyResetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
