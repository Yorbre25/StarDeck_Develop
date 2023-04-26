import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultipleClickableCardsComponent } from './multiple-clickable-cards.component';

describe('MultipleClickableCardsComponent', () => {
  let component: MultipleClickableCardsComponent;
  let fixture: ComponentFixture<MultipleClickableCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultipleClickableCardsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MultipleClickableCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
