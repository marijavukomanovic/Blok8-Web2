import { TestBed } from '@angular/core/testing';

import { RedVoznjeService } from './red-voznje.service';

describe('RedVoznjeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RedVoznjeService = TestBed.get(RedVoznjeService);
    expect(service).toBeTruthy();
  });
});
