import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DodavanjeStaniceComponent } from './dodavanje-stanice.component';

describe('DodavanjeStaniceComponent', () => {
  let component: DodavanjeStaniceComponent;
  let fixture: ComponentFixture<DodavanjeStaniceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DodavanjeStaniceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DodavanjeStaniceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
