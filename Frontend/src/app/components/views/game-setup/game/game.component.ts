import { Component } from '@angular/core';
import { CardInt } from 'src/app/components/interfaces/card.interface';
import { HttpClient } from '@angular/common/http';
import { ApiService } from 'src/app/components/services/api.service';
import { PlanetInterface } from 'src/app/components/interfaces/planet.interface';
import { DeckInterface } from 'src/app/components/interfaces/deck.interface';
import { selected_Card_S } from 'src/app/components/services/selected_card.service';
@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent {

  
  cards!: CardInt[];
  deck!: DeckInterface;
  planets!: PlanetInterface[];
  opponentName!: string;
  currentUserName!: string;
  opponentPhoto!: string;
  currentUserPhoto!: string; 
  sampleSingleCard!: CardInt[];
  cardsPerPlanet: CardInt[][] = [[],[],[]]; 

  timeExpired: boolean = false;
  bet: number = 0;
  turn:number = 1;
  totalTurns:number = 8;
  remainingCards:number = 18;
  remainingTime:number = 20;
  unhideTurn:number = 3;



  constructor(private api: ApiService, private http: HttpClient, private SCard: selected_Card_S) {
    //console.log(this.cards)
  }

  ngOnInit(): void {

    this.opponentName = "Opponent";
    this.currentUserName = "Current User";

    this.http.get('assets/samples/sampleCards.json').subscribe((data: any) => {
      console.log(data);
      this.cards = data
    });

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
  
}

onPlanetClicked(planetIndex: number) {
  console.log('Clicked planet:', planetIndex);
 this.remainingTime = 40;
// this.api.getCard(this.SCard.getCard()); 
this.cardsPerPlanet[planetIndex].push(this.sampleSingleCard[0])

}

}