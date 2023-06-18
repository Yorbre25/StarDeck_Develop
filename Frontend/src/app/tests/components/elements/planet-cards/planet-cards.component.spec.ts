import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanetCardsComponent } from '../../../../components/elements/planet-cards/planet-cards.component';

describe('PlanetCardsComponent', () => {
  let component: PlanetCardsComponent;
  let fixture: ComponentFixture<PlanetCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanetCardsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlanetCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
