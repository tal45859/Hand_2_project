/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ImageValidationService } from './ImageValidation.service';

describe('Service: ImageValidation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ImageValidationService]
    });
  });

  it('should ...', inject([ImageValidationService], (service: ImageValidationService) => {
    expect(service).toBeTruthy();
  }));
});
