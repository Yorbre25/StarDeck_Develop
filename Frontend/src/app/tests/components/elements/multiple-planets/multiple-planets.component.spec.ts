import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultiplePlanetsComponent } from '../../../../components/elements/multiple-planets/multiple-planets.component';

describe('MultiplePlanetsComponent', () => {
  let component: MultiplePlanetsComponent;
  let fixture: ComponentFixture<MultiplePlanetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultiplePlanetsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MultiplePlanetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
