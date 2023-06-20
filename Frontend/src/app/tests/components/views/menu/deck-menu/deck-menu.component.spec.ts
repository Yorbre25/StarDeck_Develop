import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeckMenuComponent } from '../../../../../components/views/menu/deck-menu/deck-menu.component';

describe('DeckMenuComponent', () => {
  let component: DeckMenuComponent;
  let fixture: ComponentFixture<DeckMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeckMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeckMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
