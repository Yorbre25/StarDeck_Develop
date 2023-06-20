import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleDeckComponent } from '../../../../components/views/single-deck/single-deck.component';

describe('SingleDeckComponent', () => {
  let component: SingleDeckComponent;
  let fixture: ComponentFixture<SingleDeckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SingleDeckComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SingleDeckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
