import { Component, Input, OnInit} from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';

@Component({
  selector: 'app-multiple-cards',
  templateUrl: './multiple-cards.component.html',
  styleUrls: ['./multiple-cards.component.scss']
})
export class MultipleCardsComponent{

  @Input()
  cards!:CardInt[];

    
  constructor() {
    
  }

}
