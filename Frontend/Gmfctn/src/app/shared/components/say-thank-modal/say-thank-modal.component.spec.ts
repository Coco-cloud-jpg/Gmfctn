import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SayThankModalComponent } from './say-thank-modal.component';

describe('SaythankModalComponent', () => {
  let component: SayThankModalComponent;
  let fixture: ComponentFixture<SayThankModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SayThankModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SayThankModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
