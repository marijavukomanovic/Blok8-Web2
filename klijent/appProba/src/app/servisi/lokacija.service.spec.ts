import { TestBed } from '@angular/core/testing';

import { LokacijaService } from './lokacija.service';

describe('LokacijaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LokacijaService = TestBed.get(LokacijaService);
    expect(service).toBeTruthy();
  });
});
