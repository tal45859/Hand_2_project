/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AllReportingComponent } from './All-Reporting.component';

describe('AllReportingComponent', () => {
  let component: AllReportingComponent;
  let fixture: ComponentFixture<AllReportingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllReportingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
