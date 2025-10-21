import { TestBed } from '@angular/core/testing';

import { Quizdata } from './quizdata';

describe('Quizdata', () => {
  let service: Quizdata;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Quizdata);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
