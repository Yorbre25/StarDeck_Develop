import { TestBed } from '@angular/core/testing';
import { CardService } from './Card.service';

describe('CardService', () => {
  let service: CardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CardService);
  });
  

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});