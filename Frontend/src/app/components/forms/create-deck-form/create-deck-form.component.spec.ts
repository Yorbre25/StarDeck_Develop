import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateDeckFormComponent } from './create-deck-form.component';

describe('CreateDeckFormComponent', () => {
  let component: CreateDeckFormComponent;
  let fixture: ComponentFixture<CreateDeckFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateDeckFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateDeckFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
