import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IzmenaCenovnikaComponent } from './izmena-cenovnika.component';

describe('IzmenaCenovnikaComponent', () => {
  let component: IzmenaCenovnikaComponent;
  let fixture: ComponentFixture<IzmenaCenovnikaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IzmenaCenovnikaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IzmenaCenovnikaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
