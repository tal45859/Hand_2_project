/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AllMyPostComponent } from './All-My-Post.component';

describe('AllMyPostComponent', () => {
  let component: AllMyPostComponent;
  let fixture: ComponentFixture<AllMyPostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllMyPostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllMyPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
