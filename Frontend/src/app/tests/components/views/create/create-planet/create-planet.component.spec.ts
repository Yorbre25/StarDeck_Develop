import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePlanetComponent } from '../../../../../components/views/create/create-planet/create-planet.component';

describe('CreatePlanetComponent', () => {
  let component: CreatePlanetComponent;
  let fixture: ComponentFixture<CreatePlanetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreatePlanetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatePlanetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
