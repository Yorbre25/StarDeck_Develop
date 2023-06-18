import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeckSelectComponent } from './deck-select.component';

describe('DeckSelectComponent', () => {
  let component: DeckSelectComponent;
  let fixture: ComponentFixture<DeckSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeckSelectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeckSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
