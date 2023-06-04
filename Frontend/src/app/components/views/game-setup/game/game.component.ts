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
  opponentPhoto!: string;
  currentUserPhoto!: string;
  sampleSingleCard!: CardInt[];
  cardsPerPlanet: CardInt[][] = [[], [], []];
  timeExpired: boolean = false;
  energy:number=20;
  unhideTurn: number = 3;
  EnergyFault:boolean=false;


  constructor(private http: HttpClient, private deckService: deckService, private gameService: gameService,
     private loginService: LoginService, private SCard: selected_Card_S, private cardService:CardService) {
    this.bet = this.defaultinitialbet;
    this.GameValues = this.gameService.getGameValues();
    this.UserInfoValues = this.gameService.getplayerinfo(this.loginService.getid())
    this.turn = this.GameValues.currentTurn;
    this.totalTurns = this.GameValues.totalTurns;
    this.remainingCards = this.defaultRemainingCards;
    this.remainingTime = this.GameValues.timePerTurn;

  }

  ngOnInit(): void {

    this.opponentName = this.UserInfoValues.OpTag;
    this.currentUserName = this.UserInfoValues.Ptag;
    this.deckname = this.UserInfoValues.PDeckN;

    this.gameService.GetGamePlanets().subscribe((data)=>{
      this.planets=data
    })

    this.gameService.GetHandCards(this.loginService.getid()).subscribe((data)=>{
      this.cards=data
    })

    this.SCard.initializeCardList()
    this.SCard.initializeSCard()
    this.cardsPerPlanet = this.cardsPerPlanet
 
}

onClickEndTurn() {
  if(this.turn!=null){
    this.turn = this.turn + 1;
    this.remainingTime = 20; 
    console.log('Clicked planet:');
    this.gameService.drawCard(this.loginService.getid())
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
