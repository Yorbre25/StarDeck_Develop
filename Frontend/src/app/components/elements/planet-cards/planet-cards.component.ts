import { Component , Input } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { seleced_Card_S } from '../../services/selected_card.service';
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

  
  cards!: CardInt[];
  //planet!: PlanetInterface;

  @Input()
  planet!:PlanetInterface;


  constructor(private api: ApiService, private http: HttpClient) {

    //console.log(this.cards)
  }


  ngOnInit(): void {

    this.http.get('assets/samples/sampleCards2.json').subscribe((data: any) => {
      console.log(data);
      this.cards = data
    });

    this.http.get('assets/samples/samplePlanets.json').subscribe((data2: any) => {
      console.log(data2);
      this.planet = data2[0]
    });
}
}


