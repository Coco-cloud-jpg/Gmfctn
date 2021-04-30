import { TestBed } from '@angular/core/testing';

import { ThankService } from './thank.service';

describe('ThankService', () => {
  let service: ThankService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ThankService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
