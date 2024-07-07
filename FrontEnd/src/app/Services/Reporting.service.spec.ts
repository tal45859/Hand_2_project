/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ReportingService } from './Reporting.service';

describe('Service: Reporting', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReportingService]
    });
  });

  it('should ...', inject([ReportingService], (service: ReportingService) => {
    expect(service).toBeTruthy();
  }));
});
