import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultipleCardsComponent } from '../../../../components/elements/multiple-cards/multiple-cards.component';

describe('MultipleCardsComponent', () => {
  let component: MultipleCardsComponent;
  let fixture: ComponentFixture<MultipleCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultipleCardsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MultipleCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
