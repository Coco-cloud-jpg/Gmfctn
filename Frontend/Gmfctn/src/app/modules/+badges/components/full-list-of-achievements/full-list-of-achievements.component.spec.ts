import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FullListOfAchievementsComponent } from './full-list-of-achievements.component';

describe('FullListOfAchievementsComponent', () => {
  let component: FullListOfAchievementsComponent;
  let fixture: ComponentFixture<FullListOfAchievementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FullListOfAchievementsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FullListOfAchievementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
