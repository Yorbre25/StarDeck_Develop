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
    energy: number|undefined; 
    cost: number|undefined; 
    type: string|null; 
    race: string|null;
    activated_card:boolean|null};

}
