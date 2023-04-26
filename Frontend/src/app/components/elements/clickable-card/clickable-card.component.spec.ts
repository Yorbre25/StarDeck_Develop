import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClickableCardComponent } from './clickable-card.component';

describe('ClickableCardComponent', () => {
  let component: ClickableCardComponent;
  let fixture: ComponentFixture<ClickableCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClickableCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClickableCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
