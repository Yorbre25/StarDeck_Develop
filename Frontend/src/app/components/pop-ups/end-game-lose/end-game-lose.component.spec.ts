import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EndGameLoseComponent } from './end-game-lose.component';

describe('EndGameLoseComponent', () => {
  let component: EndGameLoseComponent;
  let fixture: ComponentFixture<EndGameLoseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EndGameLoseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EndGameLoseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
