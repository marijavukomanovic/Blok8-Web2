import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DodavanjeCenovnikaComponent } from './dodavanje-cenovnika.component';

describe('DodavanjeCenovnikaComponent', () => {
  let component: DodavanjeCenovnikaComponent;
  let fixture: ComponentFixture<DodavanjeCenovnikaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DodavanjeCenovnikaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DodavanjeCenovnikaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
