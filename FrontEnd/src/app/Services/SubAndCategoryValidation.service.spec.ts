/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SubAndCategoryValidationService } from './SubAndCategoryValidation.service';

describe('Service: SubAndCategoryValidation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SubAndCategoryValidationService]
    });
  });

  it('should ...', inject([SubAndCategoryValidationService], (service: SubAndCategoryValidationService) => {
    expect(service).toBeTruthy();
  }));
});
