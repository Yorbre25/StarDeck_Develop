import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatchNotFoundComponent } from 'src/app/components/pop-ups/match-not-found/match-not-found.component';

@Component({
  selector: 'app-loading-game',
  templateUrl: './loading-game.component.html',
  styleUrls: ['./loading-game.component.scss']
})
export class LoadingGameComponent {

  remainingTime: number = 20;
  timeExpired: boolean = false;
  showPopUp: boolean = false;

  constructor(public dialog: MatDialog) {
  }

  ngOnInit() {
    setInterval(() => {
      this.remainingTime--;
      if (this.remainingTime == 0) {
        this.timeExpired = true;
       this.openPopup()
      }
    }, 1000);
  }

  openPopup(): void {
    const dialogRef = this.dialog.open(MatchNotFoundComponent, {
      width: '250px',
      data: { message: 'El tiempo de espera ha expirado.' }
    });
  }
}