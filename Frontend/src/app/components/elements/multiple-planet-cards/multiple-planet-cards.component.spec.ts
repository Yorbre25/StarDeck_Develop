import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultiplePlanetCardsComponent } from './multiple-planet-cards.component';

describe('MultiplePlanetCardsComponent', () => {
  let component: MultiplePlanetCardsComponent;
  let fixture: ComponentFixture<MultiplePlanetCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultiplePlanetCardsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MultiplePlanetCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
