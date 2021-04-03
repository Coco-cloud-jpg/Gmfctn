import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaythankModalComponent } from './saythank-modal.component';

describe('SaythankModalComponent', () => {
  let component: SaythankModalComponent;
  let fixture: ComponentFixture<SaythankModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SaythankModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SaythankModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
