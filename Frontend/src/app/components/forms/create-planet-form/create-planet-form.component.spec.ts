import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePlanetFormComponent } from './create-planet-form.component';

describe('CreatePlanetFormComponent', () => {
  let component: CreatePlanetFormComponent;
  let fixture: ComponentFixture<CreatePlanetFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreatePlanetFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatePlanetFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
