import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LokacijaComponent } from './lokacija.component';

describe('LokacijaComponent', () => {
  let component: LokacijaComponent;
  let fixture: ComponentFixture<LokacijaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LokacijaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LokacijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
