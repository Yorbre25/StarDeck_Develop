import { Component, Input, OnInit} from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';

/**
 * @description
 * This component displays multiple cards in a matrix. The cards displayed are input by an external component. 
 * The cards are displayed using the card-component. 
 *  
 * @example
<div class="multiple-cards">
    <app-card class="app-card" *ngFor="let card of cards" [element]="card">
    Cards are displayed. 
    </app-card>
</div>
*/
@Component({
  selector: 'app-multiple-cards',
  templateUrl: './multiple-cards.component.html',
  styleUrls: ['./multiple-cards.component.scss']
})
export class MultipleCardsComponent{

  @Input()
  cards!:CardInt[];

  @Input() clickable: boolean = false;

    
  constructor() {
    
  }

}
