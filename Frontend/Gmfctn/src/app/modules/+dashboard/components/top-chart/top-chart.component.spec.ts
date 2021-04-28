import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopChartComponent } from './top-chart.component';

describe('TopChartComponent', () => {
  let component: TopChartComponent;
  let fixture: ComponentFixture<TopChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TopChartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TopChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
