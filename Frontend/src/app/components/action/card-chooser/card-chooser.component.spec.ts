import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardChooserComponent } from './card-chooser.component';

describe('CardChooserComponent', () => {
  let component: CardChooserComponent;
  let fixture: ComponentFixture<CardChooserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardChooserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardChooserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
