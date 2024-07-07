/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserValidationService } from './UserValidation.service';

describe('Service: UserValidation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserValidationService]
    });
  });

  it('should ...', inject([UserValidationService], (service: UserValidationService) => {
    expect(service).toBeTruthy();
  }));
});
