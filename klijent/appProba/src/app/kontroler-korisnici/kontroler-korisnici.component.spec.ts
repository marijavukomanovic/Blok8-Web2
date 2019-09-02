import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KontrolerKorisniciComponent } from './kontroler-korisnici.component';

describe('KontrolerKorisniciComponent', () => {
  let component: KontrolerKorisniciComponent;
  let fixture: ComponentFixture<KontrolerKorisniciComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KontrolerKorisniciComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KontrolerKorisniciComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
