import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EndGameComponent } from '../../../../components/pop-ups/end-game/end-game.component';

describe('EndGameComponent', () => {
  let component: EndGameComponent;
  let fixture: ComponentFixture<EndGameComponent>;
  let compiled: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EndGameComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EndGameComponent);
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
