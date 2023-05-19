import { Component , Input } from '@angular/core';
import { CardInt } from 'src/app/components/interfaces/card.interface';
import { HttpClient } from '@angular/common/http';
import { PlanetInterface } from 'src/app/components/interfaces/planet.interface';
@Component({
  selector: 'app-planet-cards',
  templateUrl: './planet-cards.component.html',
  styleUrls: ['./planet-cards.component.scss']
})
export class PlanetCardsComponent {

  
  
  //planet!: PlanetInterface;

  hidden!:PlanetInterface;

  @Input()
  planet!:PlanetInterface;
  cards!: CardInt[];
  currentUserPoints!: number;
  opponentPoints!: number;
  opponentName!: string;
  currentUserName!: string;


  constructor(private http: HttpClient) {

    //console.log(this.cards)
  }


  ngOnInit(): void {

    this.currentUserPoints = 0; 
    this.opponentPoints = 0;
    this.opponentName = "Opponent";
    this.currentUserName = "Current User";

    this.http.get('assets/samples/sampleCards2.json').subscribe((data: any) => {
      console.log(data);
      this.cards = []
    });

  

    this.http.get('assets/game/hiddenPlanet.json').subscribe((data3: any) => {
      console.log(data3);
      this.hidden = data3
    });
}
}


