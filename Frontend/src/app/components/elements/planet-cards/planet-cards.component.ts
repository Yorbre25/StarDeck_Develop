import { Component , Input } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { selected_Card_S } from '../../services/selected_card.service';
import { CardInt } from 'src/app/components/interfaces/card.interface';
import { HttpClient } from '@angular/common/http';
import { ApiService } from 'src/app/components/services/api.service';
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


  constructor(private api: ApiService, private http: HttpClient) {

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


