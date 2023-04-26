import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent {

  @Input()
  element!: { name: string|null; 
    image: string|null; 
    description: string|null; 
    energy: string|null; 
    price: string|null; 
    type: string|null; 
    race: string|null; };

}
