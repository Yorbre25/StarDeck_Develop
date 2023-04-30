import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { CardInt } from '../../interfaces/card.interface';
import { LoginService } from '../../services/login.service';
import { seleced_Card_S } from '../../services/selected_card.service';

/**
 * @description
 * This component displays content belonging to an existing card from 
 * the database inside a rectangular linear-gradient colored block. 
 * The content displayed includes: name, description, and character image, energy, cost, 
 * type and race. 
*/
@Component({
  selector: 'app-planet',
  templateUrl: './planet.component.html',
  styleUrls: ['./planet.component.scss']
})
export class PlanetComponent {
  @Input()
  element!: {
    id: string | null;
    name: string | null;
    image: string | null;
    type: string | null;
    description: string | null;
    activated_planet: boolean | null;
  };

  @Input() clickable: boolean = false;


  constructor(private router: Router, private api: ApiService, private logins:LoginService, private Scard:seleced_Card_S) {
  }

}

