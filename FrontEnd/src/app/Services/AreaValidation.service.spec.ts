/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AreaValidationService } from './AreaValidation.service';

describe('Service: AreaValidation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AreaValidationService]
    });
  });

  it('should ...', inject([AreaValidationService], (service: AreaValidationService) => {
    expect(service).toBeTruthy();
  }));
});
