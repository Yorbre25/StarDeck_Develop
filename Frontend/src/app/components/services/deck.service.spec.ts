import { TestBed } from '@angular/core/testing';
import { deckService } from './deck.service';

describe('deckService', () => {
  let service: deckService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(deckService);
  });
  

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});