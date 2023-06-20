import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchNotFoundComponent } from '../../../../components/pop-ups/match-not-found/match-not-found.component';

describe('MatchNotFoundComponent', () => {
  let component: MatchNotFoundComponent;
  let fixture: ComponentFixture<MatchNotFoundComponent>;
  let compiled: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MatchNotFoundComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MatchNotFoundComponent);
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
