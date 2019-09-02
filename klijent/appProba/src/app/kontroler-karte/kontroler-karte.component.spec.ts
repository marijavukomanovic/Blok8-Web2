import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KontrolerKarteComponent } from './kontroler-karte.component';

describe('KontrolerKarteComponent', () => {
  let component: KontrolerKarteComponent;
  let fixture: ComponentFixture<KontrolerKarteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KontrolerKarteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KontrolerKarteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
