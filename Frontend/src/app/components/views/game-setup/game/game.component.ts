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
  bet!:number;
  turn!:number;
  totalTurns!:number;
  remainingCards!:number;
  timeRemaining!:number;


  constructor(private api: ApiService, private http: HttpClient) {
    this.bet = 0;
    this.turn = 1;
    this.totalTurns = 8;
    this.remainingCards = 18;
    this.timeRemaining = 20;

    //console.log(this.cards)
  }

  ngOnInit(): void {

    this.http.get('assets/samples/sampleCards.json').subscribe((data: any) => {
      console.log(data);
      this.cards = data
    });

    this.http.get('assets/samples/samplePlanets.json').subscribe((data2: any) => {
      console.log(data2);
      this.planets = data2
    });
}
}