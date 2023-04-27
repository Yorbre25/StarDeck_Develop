import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { CardInt } from '../../interfaces/card.interface';
/**
 * @description
 * This component displays content belonging to an existing card from 
 * the database inside a rectangular linear-gradient colored block. 
 * The content displayed includes: name, description, and character image, energy, cost, 
 * type and race. 
*/
@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent {


  @Input()
  element!: {
    id: string | null;
    name: string | null;
    image: string | null;
    description: string | null;
    energy: number | undefined;
    cost: number | undefined;
    type: string | null;
    race: string | null;
    activated_card: boolean | null
  };

  @Input() clickable: boolean = false;


  constructor(private router: Router, private api: ApiService) {
  }


  onClick() {
    if (this.clickable) {


      this.api.addCard(this.element);
      this.router.navigate(['/cards']);
    }


  }
}