import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatchNotFoundComponent } from 'src/app/components/pop-ups/match-not-found/match-not-found.component';
import { MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
import { gameService } from 'src/app/components/services/Game.service';
@Component({
  selector: 'app-loading-game',
  templateUrl: './loading-game.component.html',
  styleUrls: ['./loading-game.component.scss']
})
export class LoadingGameComponent {

  remainingTime: number = 20;
  timeExpired: boolean = false;
  showPopUp: boolean = false;

  constructor(private router: Router, public dialog: MatDialog, protected gameService:gameService) {
  }

  ngOnInit() {
    setInterval(() => {
      this.remainingTime--;
      if (this.remainingTime == 0) {
        this.timeExpired = true;
        console.info('Popup disabled.');
        //this.openPopup() // esto esta desactivado para poder hacer pruebas 
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
    
    console.log("Before")
    this.gameService.setgameID("G-oh7ikpwlscny")
    console.log("After")
    this.gameService.SetUpHands().subscribe((data)=>{
      
    })

    const uuid = uuidv4();
    console.log(uuid);
    this.router.navigate(['/match', uuid]);
  
  }
}