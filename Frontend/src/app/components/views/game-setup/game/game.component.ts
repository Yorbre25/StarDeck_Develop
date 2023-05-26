import { Component } from '@angular/core';
import { CardInt } from 'src/app/components/interfaces/card.interface';
import { HttpClient } from '@angular/common/http';
import { ApiService } from 'src/app/components/services/api.service';
import { PlanetInterface } from 'src/app/components/interfaces/planet.interface';
import { DeckInterface } from 'src/app/components/interfaces/deck.interface';
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

  timeExpired: boolean = false;
  bet: number = 0;
  turn:number = 1;
  totalTurns:number = 8;
  remainingCards:number = 18;
  remainingTime:number = 20;
  unhideTurn:number = 3;



  constructor(private api: ApiService, private http: HttpClient) {
    //console.log(this.cards)
  }

  ngOnInit(): void {

    this.opponentName = "Opponent";
    this.currentUserName = "Current User";

    this.http.get('assets/samples/sampleCards.json').subscribe((data: any) => {
      console.log(data);
      this.cards = data
    });

    this.http.get('assets/samples/samplePlanets.json').subscribe((data2: any) => {
      console.log(data2);
      this.planets = data2
    });

    setInterval(() => {
      this.remainingTime--;
      if (this.remainingTime == 0) {
        this.timeExpired = true;
       this.onClickEndTurn()
      }
      if (this.turn >= this.unhideTurn) {
        this.planets[2].show = true; // esto hay que mandarlo al api 
      }
      if (this.turn >= this.totalTurns) {
        // this.openPopup() // esto esta desactivado para poder hacer pruebas 
      }
    }, 1000);
}

onClickEndTurn() {
  this.turn = this.turn + 1;
  this.remainingTime = 20; 
  // mandar al api info :) 
  
}
}