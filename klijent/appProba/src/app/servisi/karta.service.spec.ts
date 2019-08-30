import { TestBed } from '@angular/core/testing';

import { KartaService } from './karta.service';

describe('KartaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: KartaService = TestBed.get(KartaService);
    expect(service).toBeTruthy();
  });
});
