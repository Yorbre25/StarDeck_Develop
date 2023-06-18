import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchNotFoundComponent } from '../../../../components/pop-ups/match-not-found/match-not-found.component';

describe('MatchNotFoundComponent', () => {
  let component: MatchNotFoundComponent;
  let fixture: ComponentFixture<MatchNotFoundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MatchNotFoundComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MatchNotFoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
