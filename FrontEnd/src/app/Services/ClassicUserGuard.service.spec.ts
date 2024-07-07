/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ClassicUserGuardService } from './ClassicUserGuard.service';

describe('Service: ClassicUserGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClassicUserGuardService]
    });
  });

  it('should ...', inject([ClassicUserGuardService], (service: ClassicUserGuardService) => {
    expect(service).toBeTruthy();
  }));
});
