import { Component, Input, OnInit} from '@angular/core';
import { PlanetInterface } from '../../interfaces/planet.interface';
import { HttpClient } from '@angular/common/http';
import { CardInt } from '../../interfaces/card.interface';


@Component({
  selector: 'app-multiple-planet-cards',
  templateUrl: './multiple-planet-cards.component.html',
  styleUrls: ['./multiple-planet-cards.component.scss']
})
export class MultiplePlanetCardsComponent {

  canBeSelected:boolean = true; 

  @Input()
  planets!:PlanetInterface[];
  @Input()
  cardsPerPlanet!:CardInt[][];

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.http.get('assets/samples/samplePlanets.json').subscribe((data: any) => {
      console.log(data);
      this.planets = data
    });
  }
}
