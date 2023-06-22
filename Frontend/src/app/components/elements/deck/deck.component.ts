import { Component, Input } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-deck',
  templateUrl: './deck.component.html',
  styleUrls: ['./deck.component.scss']
})
export class DeckComponent {

  //deck!:DeckInterface[];
  cards!:CardInt[];
  

  constructor(private logs:LoginService) {
      
    //console.log(this.cards)
  }

  ngOnInit(): void {
    //this.api.getAllDecks(this.logs.getid()).subscribe(data => {
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