import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EndGameTieComponent } from './end-game-tie.component';

describe('EndGameTieComponent', () => {
  let component: EndGameTieComponent;
  let fixture: ComponentFixture<EndGameTieComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EndGameTieComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EndGameTieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
