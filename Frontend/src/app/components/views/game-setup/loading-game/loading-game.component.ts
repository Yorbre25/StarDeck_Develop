import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatchNotFoundComponent } from 'src/app/components/pop-ups/match-not-found/match-not-found.component';
import { MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
import { gameService } from 'src/app/components/services/Game.service';
import { LoginService } from 'src/app/components/services/login.service';
import { deckService } from 'src/app/components/services/deck.service';
import { map } from 'rxjs';
@Component({
  selector: 'app-loading-game',
  templateUrl: './loading-game.component.html',
  styleUrls: ['./loading-game.component.scss']
})
export class LoadingGameComponent {

  remainingTime: number = 20;
  timeExpired: boolean = false;
  showPopUp: boolean = false;

  constructor(private loginService:LoginService,private deckService:deckService, private router: Router, public dialog: MatDialog, protected gameService:gameService) {
  }

  ngOnInit() {
    this.gameService.SearchGame(this.loginService.getid(),this.deckService.getDeck()).subscribe((data)=>{
      
      var nInterval=setInterval(() => {
        let gameID:string|null=""
        this.remainingTime--;
        if (this.remainingTime == 0) {
          this.timeExpired = true;
          console.info('Popup disabled.');
          clearInterval(nInterval);
          this.openPopup() // esto esta desactivado para poder hacer pruebas 
        }
        this.gameService.GetAllGames().subscribe((data)=>{
          for (var Game of data){
            if (this.loginService.getid()!=null && (this.loginService.getid()==Game.player1Id || this.loginService.getid()==Game.player2Id)){
                gameID=Game.id
                this.gameService.SetParameters(Game.player1Id,Game.player2Id,Game.id)   
                console.log(this.gameService.GameInfo)         
            }
          }
          if(gameID!=undefined && gameID!=""){
            this.gameService.setgameID(gameID)
            const uuid = uuidv4();
            console.log(uuid);
            clearInterval(nInterval);
            this.router.navigate(['/match', uuid]);
          }
        })
      }, 1000);      
    });
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
    
    this.gameService.setgameID("G-4qxupc239iqp")
    this.gameService.SetUpHands().subscribe((data)=>{
      
    })

    const uuid = uuidv4();
    console.log(uuid);
    this.router.navigate(['/match', uuid]);
  
  }
}