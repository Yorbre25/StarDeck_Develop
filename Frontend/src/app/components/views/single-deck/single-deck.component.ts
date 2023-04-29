import { Component, Input } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';
import { ApiService } from '../../services/api.service';
import { DeckInterface } from '../../interfaces/deck.interface';

@Component({
  selector: 'app-single-deck',
  templateUrl: './single-deck.component.html',
  styleUrls: ['./single-deck.component.scss']
})
export class SingleDeckComponent {


  //deck!:DeckInterface[];
  cards!: CardInt[];

  constructor(private api: ApiService) {

    //console.log(this.cards)
  }

  ngOnInit(): void {
    //this.api.getDeck(id).subscribe(data => {
    // console.log(data)
    //this.deck = data 
    //});
  }

  @Input()
  deck!: {
    id: string | null;
    name: string | null;
    cards: CardInt[] | null;

  }
}