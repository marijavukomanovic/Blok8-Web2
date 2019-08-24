import { TestBed } from '@angular/core/testing';

import { CenovnikService } from './cenovnik.service';

describe('CenovnikService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CenovnikService = TestBed.get(CenovnikService);
    expect(service).toBeTruthy();
  });
});
