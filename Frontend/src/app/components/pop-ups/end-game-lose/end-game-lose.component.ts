import { Component, Inject, OnInit } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { LoadingGameComponent } from '../../views/game-setup/loading-game/loading-game.component';
import { Router } from '@angular/router';
import { GameComponent } from '../../views/game-setup/game/game.component';
@Component({
  selector: 'app-end-game-lose',
  templateUrl: './end-game-lose.component.html',
  styleUrls: ['./end-game-lose.component.scss']
})

export class EndGameLoseComponent implements OnInit{

  gameStatus: boolean = true;
  WinOrLoseMessage!: string;
  opponentName!: string | null;
  currentUserName!: string | null;

  constructor(private router: Router, private dialogRef: MatDialogRef<GameComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {
    //this.gameStatus = data.game_state
    if (this.gameStatus){
      //this.WinOrLoseMessage = "Victoria!!"
    } else {
      //this.WinOrLoseMessage = "Derrota :("
    }
    }
  ngOnInit(): void {
    this.gameStatus = true;
  }

  close() {
    this.dialogRef.close(true);
    this.router.navigate(['/home']);
    
}
}
