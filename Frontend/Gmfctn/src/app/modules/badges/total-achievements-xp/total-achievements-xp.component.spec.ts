import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TotalAchievementsXpComponent } from './total-achievements-xp.component';

describe('TotalAchievementsXpComponent', () => {
  let component: TotalAchievementsXpComponent;
  let fixture: ComponentFixture<TotalAchievementsXpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TotalAchievementsXpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TotalAchievementsXpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
