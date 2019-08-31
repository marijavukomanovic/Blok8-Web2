import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DodavanjeLinijeComponent } from './dodavanje-linije.component';

describe('DodavanjeLinijeComponent', () => {
  let component: DodavanjeLinijeComponent;
  let fixture: ComponentFixture<DodavanjeLinijeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DodavanjeLinijeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DodavanjeLinijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
