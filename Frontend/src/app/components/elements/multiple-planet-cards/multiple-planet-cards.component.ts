import { Component, Input, OnInit} from '@angular/core';
import { PlanetInterface } from '../../interfaces/planet.interface';


@Component({
  selector: 'app-multiple-planet-cards',
  templateUrl: './multiple-planet-cards.component.html',
  styleUrls: ['./multiple-planet-cards.component.scss']
})
export class MultiplePlanetCardsComponent {
  @Input()
  planets!:PlanetInterface[];

}
