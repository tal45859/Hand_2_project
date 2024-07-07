/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AllPostComponent } from './All-Post.component';

describe('AllPostComponent', () => {
  let component: AllPostComponent;
  let fixture: ComponentFixture<AllPostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllPostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
