import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BackButtonComponent } from '../../../../components/elements/back-button/back-button.component';

describe('BackButtonComponent', () => {
  let component: BackButtonComponent;
  let fixture: ComponentFixture<BackButtonComponent>;
  let compiled: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BackButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BackButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    compiled = fixture.nativeElement;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('match with snapshot', () => {
    expect(compiled).toMatchSnapshot();
  });
});
