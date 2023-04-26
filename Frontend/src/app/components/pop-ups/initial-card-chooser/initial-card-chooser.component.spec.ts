import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InitialCardChooserComponent } from './initial-card-chooser.component';

describe('InitialCardChooserComponent', () => {
  let component: InitialCardChooserComponent;
  let fixture: ComponentFixture<InitialCardChooserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InitialCardChooserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InitialCardChooserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
