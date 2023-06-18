import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanetMenuComponent } from './planet-menu.component';

describe('PlanetMenuComponent', () => {
  let component: PlanetMenuComponent;
  let fixture: ComponentFixture<PlanetMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanetMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlanetMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
