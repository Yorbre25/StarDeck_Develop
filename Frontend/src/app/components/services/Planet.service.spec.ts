import { TestBed } from '@angular/core/testing';
import { planetService } from './Planet.service';

describe('planetService', () => {
  let service: planetService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(planetService);
  });
  

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});