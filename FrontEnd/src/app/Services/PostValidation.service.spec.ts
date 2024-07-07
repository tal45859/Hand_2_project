/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PostValidationService } from './PostValidation.service';

describe('Service: PostValidation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PostValidationService]
    });
  });

  it('should ...', inject([PostValidationService], (service: PostValidationService) => {
    expect(service).toBeTruthy();
  }));
});
