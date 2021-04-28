import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExoftAchievementsComponent } from './exoft-achievements.component';

describe('ExoftAchievementsComponent', () => {
  let component: ExoftAchievementsComponent;
  let fixture: ComponentFixture<ExoftAchievementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExoftAchievementsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExoftAchievementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
