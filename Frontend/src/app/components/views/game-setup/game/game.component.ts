import { Component } from '@angular/core';
import { CardInt } from 'src/app/components/interfaces/card.interface';
import { HttpClient } from '@angular/common/http';
import { gameService } from 'src/app/components/services/Game.service';
import { deckService } from 'src/app/components/services/deck.service';
import { LoginService } from 'src/app/components/services/login.service';
import { PlanetInterface } from 'src/app/components/interfaces/planet.interface';
import { DeckInterface } from 'src/app/components/interfaces/deck.interface';
import { selected_Card_S } from 'src/app/components/services/selected_card.service';
import { CardService } from 'src/app/components/services/Card.service';
import { UsersInfoGame } from 'src/app/components/interfaces/GameUsersInfo.interface';
import { RouterTestingHarness } from '@angular/router/testing';
import { ignoreElements } from 'rxjs';
import { MatDialogConfig } from '@angular/material/dialog';
import { MatDialog } from '@angular/material/dialog';
import { EndGameComponent } from 'src/app/components/pop-ups/end-game/end-game.component';
import { EndGameLoseComponent } from 'src/app/components/pop-ups/end-game-lose/end-game-lose.component';
import { EndGameTieComponent } from 'src/app/components/pop-ups/end-game-tie/end-game-tie.component';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent {

  private defaultinitialbet: number = 0;
  private defaultRemainingCards: number = 18;
  private GameValues: any;
  private UserInfoValues!: UsersInfoGame;

  cards!: CardInt[];
  deck!: DeckInterface;
  planets!: PlanetInterface[];
  bet!: number;
  turn!: number | null;
  totalTurns!: number | null;
  remainingCards!: number | null;
  remainingTime!: number | null;
  opponentName!: string | null;
  currentUserName!: string | null;
  deckname!: string | null;
  deckid!:string|null;
  opponentPhoto!: string;
  currentUserPhoto!: string;
  sampleSingleCard!: CardInt[];
  cardsPerPlanet: CardInt[][] = [[], [], []];
  timeExpired: boolean = false;
  energy:number=20;
  unhideTurn: number = 3;
  EnergyFault:boolean=false;
  gameCurrentlyActive=true;
  gameState:string="Empate";


  constructor(private http: HttpClient, private deckService: deckService, private gameService: gameService,
    private loginService: LoginService, private SCard: selected_Card_S, public dialog: MatDialog, private cardService:CardService) {
    this.bet = this.defaultinitialbet;
    this.gameService.GetGamePlayers().subscribe((data)=>{
      for (var Game of data){
          if(Game.gameId==gameService.GameInfo.id){
              if(Game.playerId==this.loginService.getid()){
                  this.deckid=Game.deckId
                  break
              }
          }
      }
    })
    this.GameValues = this.gameService.getGameValues();
    this.UserInfoValues = this.gameService.getplayerinfo(this.loginService.getid())
    this.turn = this.GameValues.currentTurn;
    this.totalTurns = this.GameValues.totalTurns;
    this.remainingCards = this.defaultRemainingCards;
    this.remainingTime = this.GameValues.timePerTurn;

  }

  ngOnInit(): void {
    this.loginService.getAllPlayers().subscribe((data)=>{
      for (var player of data){
        if(player.id==this.UserInfoValues.OpTag){
          this.opponentName=player.username
        }else if(player.id==this.UserInfoValues.Ptag){
          this.currentUserName=player.username
        }
      }
    })
    this.deckService.getAllDecks(this.loginService.getid()).subscribe((data)=>{
      for (var deck of data){
        if(this.deckid==deck.id){
          this.deckname=deck.name
        }
      }
    })
    
    this.gameService.GetGamePlanets().subscribe((data)=>{
      this.planets=data
    })
    

    this.gameService.GetHandCards(this.loginService.getid()).subscribe((data)=>{
      this.cards=data
    })

    this.SCard.initializeCardList()
    this.SCard.initializeSCard()

    
      setInterval(() => {
        if (this.remainingTime != null){
          if(this.gameCurrentlyActive){
          this.remainingTime--;
          }
          if (this.remainingTime == 0) {
            this.timeExpired = true;
            if(this.gameCurrentlyActive){
             //this.onClickEndTurn()
            }
          }
        }
      }, 1000);
    
    
 
}

onClickEndTurn() {
  if(this.turn!=null){
    this.turn = this.turn + 1;
    if(this.totalTurns != null && this.turn > this.totalTurns){
      this.endGame()
    } else {
      this.remainingTime = 20; 
      console.log('Clicked planet:');
      this.gameService.drawCard(this.loginService.getid())
    }
    
  }

  // mandar al api info :) 
  

    /**
    this.gameService.GetHandCards(this.loginService.getid()).subscribe((data)=>{
      console.log("Hand:")
      console.log(data)
      this.cards = data;
    }) */
    
    /**
    this.http.get('assets/samples/samplePlanets.json').subscribe((data2: any) => {
      console.log("Planets:")
      console.log(data2);
      this.planets = data2
    });
     */
}

  endGame() : void {

      this.gameCurrentlyActive = false;
      const dialogConfig = new MatDialogConfig();
      dialogConfig.disableClose = true;
      dialogConfig.autoFocus = true;
      dialogConfig.maxHeight = 500;
      dialogConfig.maxWidth = 1100;
      dialogConfig.width = '800px'; // Set the width to 1000 pixels
      dialogConfig.height = '350px'; // Set the height to 1000 pixels
      dialogConfig.panelClass = 'dialog-container'; // Apply custom styles to the dialog container
      dialogConfig.data = {game_state: this.gameState}

      if(this.gameState == "Win"){
        this.dialog.open(EndGameComponent, dialogConfig);
      }
      else if(this.gameState == "Lose"){
        this.dialog.open(EndGameLoseComponent, dialogConfig);
      } else {
        this.dialog.open(EndGameTieComponent, dialogConfig);
      }
      
  
  }
  

  onPlanetClicked(planetIndex: number) {
    console.log('Clicked planet:', planetIndex);
    let AddCards =true
    let CardIDtodelete=this.SCard.getcard()
    
    for(var Card of this.cardsPerPlanet[planetIndex]){
      if (CardIDtodelete==""){
        break
      }
      else if(Card.id==CardIDtodelete&&Card.energy!=undefined){//Card already inside planet
        const indexdeleteCard=this.cardsPerPlanet[planetIndex].indexOf(Card)
        this.cardsPerPlanet[planetIndex].splice(indexdeleteCard,1)
        this.cards.push(Card)//returns card to hand
        this.energy+=Card.energy
        this.SCard.initializeSCard()
        AddCards=false
      }
    }
    if(AddCards){
      this.remainingTime = 40;
      let totalenergy=0
      let CardsClicked=this.SCard.getcardList();
      let energyonLimit=true;
      this.SCard.initializeCardList()
      for(var Card of this.cards){
        if(Card.id!=null && CardsClicked.includes(Card.id)&& Card.energy!=undefined){
          totalenergy+=Card.energy
        }
        if(totalenergy>this.energy){
          energyonLimit=false
          break
        }
      }
      if(energyonLimit){
        //Delete cards from hand
        this.EnergyFault=false
        for (let i = this.cards.length - 1; i >= 0; i--) {
          const card = this.cards[i];
          if (card.id != null && CardsClicked.includes(card.id)) {
            this.cards.splice(i, 1);
          }
        }
        
        for (var newCard of CardsClicked){
          this.cardService.getCard(newCard).subscribe((data)=>{
            this.cardsPerPlanet[planetIndex].push(data)
            console.log(this.cards)  
            if(data.energy!=undefined){
              this.energy-=data.energy
            }
          }) 
          
        }
      }else{
        this.EnergyFault=true
        this.SCard.initializeCardList()
        this.SCard.initializeSCard()
      }
    }
  }
}
