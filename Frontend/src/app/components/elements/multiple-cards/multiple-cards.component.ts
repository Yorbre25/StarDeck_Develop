import { Component } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';

@Component({
  selector: 'app-multiple-cards',
  templateUrl: './multiple-cards.component.html',
  styleUrls: ['./multiple-cards.component.scss']
})
export class MultipleCardsComponent {
  cards: CardInt[] = [
    { name: "Nyan Cat",
      image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
      energy:"100",
      price:"500",
      type:"UR",
      race:"Nyan",
      description: "Nyanyanyanyanyanyanya!" },
    { name: "Nyan Cat",
    image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
    energy:"100",
    price:"500",
    type:"UR",
    race:"Nyan",
    description: "Nyanyanyanyanyanyanya!" },
    { name: "Nyan Cat",
    image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
    energy:"100",
    price:"500",
    type:"UR",
    race:"Nyan",
    description: "Nyanyanyanyanyanyanya!" },
    { name: "Nyan Cat",
    image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
    energy:"100",
    price:"500",
    type:"UR",
    race:"Nyan",
    description: "Nyanyanyanyanyanyanya!" }
  ];
}
