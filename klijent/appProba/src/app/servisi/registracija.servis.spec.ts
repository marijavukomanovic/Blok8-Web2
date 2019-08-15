import { TestBed } from '@angular/core/testing';

import { RegistracijaServis } from './registracija.servis';

describe('RegistracijaServis', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RegistracijaServis = TestBed.get(RegistracijaServis);
    expect(service).toBeTruthy();
  });
});