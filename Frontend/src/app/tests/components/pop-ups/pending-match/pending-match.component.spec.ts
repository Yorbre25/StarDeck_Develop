import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingMatchComponent } from '../../../../components/pop-ups/pending-match/pending-match.component';

describe('PendingMatchComponent', () => {
  let component: PendingMatchComponent;
  let fixture: ComponentFixture<PendingMatchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PendingMatchComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PendingMatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
