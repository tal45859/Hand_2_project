/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LoginHistoryService } from './LoginHistory.service';

describe('Service: LoginHistory', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoginHistoryService]
    });
  });

  it('should ...', inject([LoginHistoryService], (service: LoginHistoryService) => {
    expect(service).toBeTruthy();
  }));
});
