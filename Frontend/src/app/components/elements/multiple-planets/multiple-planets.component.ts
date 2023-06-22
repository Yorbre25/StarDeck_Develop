import { Component, Input, OnInit} from '@angular/core';
import { PlanetInterface } from '../../interfaces/planet.interface';

@Component({
  selector: 'app-multiple-planets',
  templateUrl: './multiple-planets.component.html',
  styleUrls: ['./multiple-planets.component.scss']
})
export class MultiplePlanetsComponent {

  @Input()
  planets!:PlanetInterface[];

  constructor() {
    
  }

}
