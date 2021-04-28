import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveTheCommentComponent } from './leave-the-comment.component';

describe('LeaveTheCommentComponent', () => {
  let component: LeaveTheCommentComponent;
  let fixture: ComponentFixture<LeaveTheCommentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaveTheCommentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveTheCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
