import { TestBed } from '@angular/core/testing';

import { KontrolerService } from './kontroler.service';

describe('KontrolerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: KontrolerService = TestBed.get(KontrolerService);
    expect(service).toBeTruthy();
  });
});
