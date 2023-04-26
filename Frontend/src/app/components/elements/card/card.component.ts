import { Component, Input } from '@angular/core';

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
    energy: string|null; 
    cost: number|undefined; 
    card_type: string|null; 
    card_race: string|null;
    activated_card:boolean|null};

}
