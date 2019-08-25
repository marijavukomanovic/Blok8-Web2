import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoKorisnikComponent } from './info-korisnik.component';

describe('InfoKorisnikComponent', () => {
  let component: InfoKorisnikComponent;
  let fixture: ComponentFixture<InfoKorisnikComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InfoKorisnikComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InfoKorisnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
