import { TestBed } from '@angular/core/testing';
import { selected_Card_S } from './selected_card.service';

describe('selected_Card_S', () => {
  let service: selected_Card_S;
  
  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(selected_Card_S);
  });
  

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});