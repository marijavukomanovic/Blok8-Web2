import { TestBed } from '@angular/core/testing';

import { LinijaService } from './linija.service';

describe('LinijaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LinijaService = TestBed.get(LinijaService);
    expect(service).toBeTruthy();
  });
});
