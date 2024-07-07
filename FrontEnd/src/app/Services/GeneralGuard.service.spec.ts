/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GeneralGuardService } from './GeneralGuard.service';

describe('Service: GeneralGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GeneralGuardService]
    });
  });

  it('should ...', inject([GeneralGuardService], (service: GeneralGuardService) => {
    expect(service).toBeTruthy();
  }));
});
