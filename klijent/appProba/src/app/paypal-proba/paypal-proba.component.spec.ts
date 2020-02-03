import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaypalProbaComponent } from './paypal-proba.component';

describe('PaypalProbaComponent', () => {
  let component: PaypalProbaComponent;
  let fixture: ComponentFixture<PaypalProbaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaypalProbaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaypalProbaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
