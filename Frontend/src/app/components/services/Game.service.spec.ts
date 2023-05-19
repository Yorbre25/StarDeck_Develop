import { TestBed } from '@angular/core/testing';
import { gameService } from './Game.service';

describe('gameService', () => {
  let service: gameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(gameService);
  });
  

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});