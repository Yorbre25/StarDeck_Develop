import { Component, Inject } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { LoadingGameComponent } from '../../views/game-setup/loading-game/loading-game.component';
import { Router } from '@angular/router';
@Component({
  selector: 'app-pending-match',
  templateUrl: './pending-match.component.html',
  styleUrls: ['./pending-match.component.scss']
})
export class PendingMatchComponent {
  constructor(private router: Router, private dialogRef: MatDialogRef<LoadingGameComponent>) {}

  close() {
    this.dialogRef.close(true);
    this.router.navigate(['/home']);
    
}
}
