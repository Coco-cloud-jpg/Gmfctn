import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalAchievmentsComponent } from './personal-achievments.component';

describe('PersonalAchievmentsComponent', () => {
  let component: PersonalAchievmentsComponent;
  let fixture: ComponentFixture<PersonalAchievmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PersonalAchievmentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonalAchievmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
