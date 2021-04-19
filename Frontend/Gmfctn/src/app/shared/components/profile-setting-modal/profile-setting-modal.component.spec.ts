import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileSettingModalComponent } from './profile-setting-modal.component';

describe('ProfileSettingModalComponent', () => {
  let component: ProfileSettingModalComponent;
  let fixture: ComponentFixture<ProfileSettingModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileSettingModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileSettingModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
