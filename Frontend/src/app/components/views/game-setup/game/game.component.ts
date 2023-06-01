import { Component } from '@angular/core';
import { CardInt } from 'src/app/components/interfaces/card.interface';
import { HttpClient } from '@angular/common/http';
import { gameService } from 'src/app/components/services/Game.service';
import { deckService } from 'src/app/components/services/deck.service';
import { LoginService } from 'src/app/components/services/login.service';
import { PlanetInterface } from 'src/app/components/interfaces/planet.interface';
import { DeckInterface } from 'src/app/components/interfaces/deck.interface';
import { selected_Card_S } from 'src/app/components/services/selected_card.service';
import { UsersInfoGame } from 'src/app/components/interfaces/GameUsersInfo.interface';
import { RouterTestingHarness } from '@angular/router/testing';

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
  unhideTurn: number = 3;


  constructor(private http: HttpClient, private deckService: deckService, private gameService: gameService,
     private loginService: LoginService, private SCard: selected_Card_S) {
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

    /** 
    this.http.get('assets/samples/sampleCards.json').subscribe((data: any) => {
      console.log(data);
      this.cards = data
    });    
    **/

    // sample cartas para planetas

    this.http.get('assets/samples/sampleSingleCard.json').subscribe((datasc: any) => {
      this.sampleSingleCard = datasc;
    });

    this.http.get('assets/samples/sampleCardsPlanet0.json').subscribe((datap0: any) => {
      this.cardsPerPlanet[0] = datap0;
    });

    this.http.get('assets/samples/sampleCardsPlanet1.json').subscribe((datap1: any) => {
      this.cardsPerPlanet[1] = datap1
    });

    this.http.get('assets/samples/sampleCardsPlanet2.json').subscribe((datap2: any) => {
      this.cardsPerPlanet[2] = datap2
    });

    this.http.get('assets/samples/samplePlanets.json').subscribe((data2: any) => {
      this.planets = data2
    });

    this.SCard.initializeCardList()
    this.SCard.cardList$.subscribe((value: string[]) => {
    })

    this.cardsPerPlanet = this.cardsPerPlanet
 
}

onClickEndTurn() {
  this.turn = this.turn + 1;
  this.remainingTime = 20; 
  console.log('Clicked planet:');
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
    
    this.gameService.GetHandCards(this.loginService.getid()).subscribe((data)=>{
      console.log("Hand:")
      console.log(data)
      this.cards = data;
    })
    
    this.gameService.GetGamePlanets().subscribe((data)=>{
      console.log("Planets:")
      console.log(data)
      this.planets=data;
    })
}


  }

  onPlanetClicked(planetIndex: number) {
    console.log('Clicked planet:', planetIndex);
    this.remainingTime = 40;
    // this.api.getCard(this.SCard.getCard()); 
    this.cardsPerPlanet[planetIndex].push(this.sampleSingleCard[0])

  }

}