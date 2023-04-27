import { Component, Input } from '@angular/core';

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
    id: string|null;
    name: string|null; 
    image: string|null; 
    description: string|null; 
    energy: number|undefined; 
    cost: number|undefined; 
    type: string|null; 
    race: string|null;
    activated_card:boolean|null};

}
