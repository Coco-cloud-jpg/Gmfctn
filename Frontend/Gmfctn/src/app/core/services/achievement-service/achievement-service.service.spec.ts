import { TestBed } from '@angular/core/testing';

import { AchievementServiceService } from './achievement-service.service';

describe('AchievementServiceService', () => {
  let service: AchievementServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AchievementServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
