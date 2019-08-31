import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BrisanjeLinijeComponent } from './brisanje-linije.component';

describe('BrisanjeLinijeComponent', () => {
  let component: BrisanjeLinijeComponent;
  let fixture: ComponentFixture<BrisanjeLinijeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BrisanjeLinijeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BrisanjeLinijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
