import { Component, Inject } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { LoadingGameComponent } from '../../views/game-setup/loading-game/loading-game.component';
import { Router } from '@angular/router';
@Component({
  selector: 'app-match-not-found',
  templateUrl: './match-not-found.component.html',
  styleUrls: ['./match-not-found.component.scss']
})
export class MatchNotFoundComponent {
  constructor(private router: Router, private dialogRef: MatDialogRef<LoadingGameComponent>) {}

  close() {
    this.dialogRef.close(true);
    this.router.navigate(['/home']);
    
}
}