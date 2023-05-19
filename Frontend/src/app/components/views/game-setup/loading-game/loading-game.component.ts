import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatchNotFoundComponent } from 'src/app/components/pop-ups/match-not-found/match-not-found.component';
import { MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
@Component({
  selector: 'app-loading-game',
  templateUrl: './loading-game.component.html',
  styleUrls: ['./loading-game.component.scss']
})
export class LoadingGameComponent {

  remainingTime: number = 5;
  timeExpired: boolean = false;
  showPopUp: boolean = false;

  constructor(private router: Router, public dialog: MatDialog) {
  }

  ngOnInit() {
    setInterval(() => {
      this.remainingTime--;
      if (this.remainingTime == 0) {
        this.timeExpired = true;
       // this.openPopup() // esto esta desactivado para poder hacer pruebas 
      }
    }, 1000);
  }

  openPopup(): void {

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.maxHeight = 500;
    dialogConfig.maxWidth = 1100;

    this.dialog.open(MatchNotFoundComponent, dialogConfig);

  }

  findMatch() {
    /**
     * 
     */
    
    const uuid = uuidv4();
    console.log(uuid);
    this.router.navigate(['/match', uuid]);

  }
}