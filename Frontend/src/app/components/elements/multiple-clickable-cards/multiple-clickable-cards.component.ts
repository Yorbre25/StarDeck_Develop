import { Component } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';
@Component({
  selector: 'app-multiple-clickable-cards',
  templateUrl: './multiple-clickable-cards.component.html',
  styleUrls: ['./multiple-clickable-cards.component.scss']
})
export class MultipleClickableCardsComponent {
  cards: CardInt[] = [
    { name: "Nyan Cat",
      image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
      energy:"100",
      price:"500",
      type:"UR",
      race:"Nyan",
      description: "Nyanyanyanyanyanyanya!" },
    { name: "Mametchi",
    image: "https://tamagotchi.com/wp-content/uploads/mametchi.jpg",
    energy:"30",
    price:"500",
    type:"SSR",
    race:"Tamagotchi",
    description: "Mametchi loves inventing things and though sometimes he fails he will succeed, he just keeps trying. He loves to study and play sports." },
    { name: "Ginjirotchi",
    image:"https://tamagotchi.com/wp-content/uploads/ginjirotchi.jpg",
    energy:"100",
    price:"500",
    type:"UR",
    race:"Nyan",
    description: "Ginjirotchi is cheerful and full of energy, though also compassionate. He loves watching dramatic movies." },
  ];
}
