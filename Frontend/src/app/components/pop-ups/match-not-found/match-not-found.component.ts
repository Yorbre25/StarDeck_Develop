import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
@Component({
  selector: 'app-match-not-found',
  templateUrl: './match-not-found.component.html',
  styleUrls: ['./match-not-found.component.scss']
})
export class MatchNotFoundComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: { message: string }) { }
}
