import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatDialogActions } from '@angular/material/dialog';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { ApiService } from '../../services/api.service';
import { CardInt } from '../../interfaces/card.interface';

/**
 * @description 
 * This component acts as dialog for user selection of initial cards. It displays the user's
 * 15 current cards and lets the user choose between 3 cards 3 times. 
 * 
 * @typedef {class} InitialCardChooserComponent
 * 
 * @property {number} cardsChosen- times user has chosen a card. 
 * @property {CardInt[]} cards - cards to be displayed. 
 * 
 * @property {Function} ngOnInIt - The function to call when the form is created calls the api service to retrieve cards.

*/


@Component({
  selector: 'app-initial-card-chooser',
  templateUrl: './initial-card-chooser.component.html',
  styleUrls: ['./initial-card-chooser.component.scss']
})
export class InitialCardChooserComponent implements OnInit{

  cardsChosen = 0;

  cards!:CardInt[];

  ngOnInit(): void {
    this.api.getAllCards().subscribe(data => {
      console.log(data)
      this.cards = data 
    });
  }



    constructor(private dialogRef: MatDialogRef<InitialCardChooserComponent>, private api: ApiService) {
      
      //console.log(this.cards)
    }

    close() {
      this.dialogRef.close();
  }
    /**
     * cards: CardInt[] = [
    { id:'',
      name: "Nyan Cat",
      image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
      energy:100,
      cost:500,
      card_type:"UR",
      card_race:"Nyan",
      description: "Nyanyanyanyanyanyanya!",
      activated_card:true}, 
    { id:'',
    name: "Mametchi",
    image: "https://tamagotchi.com/wp-content/uploads/mametchi.jpg",
    energy:30,
    cost:500,
    card_type:"SSR",
    card_race:"Tamagotchi",
    description: "Mametchi loves inventing things and though sometimes he fails he will succeed, he just keeps trying. He loves to study and play sports.",
    activated_card:true },
    { id:'',
    name: "Ginjirotchi",
    image:"https://tamagotchi.com/wp-content/uploads/ginjirotchi.jpg",
    energy:100,
    cost:500,
    card_type:"UR",
    card_race:"Nyan",
    description: "Ginjirotchi is cheerful and full of energy, though also compassionate. He loves watching dramatic movies.",
    activated_card:true},
    { id:'', 
    name: "Korok",
    image:"https://static.wikia.nocookie.net/zelda_gamepedia_en/images/b/b0/TWW_Makar_Artwork.png",
    energy:100,
    cost:5000,
    card_type:"MR",
    card_race:"Korok",
    description: "The Koroks are a race in The Legend of Zelda series. They are small, wooden people who wear leaf masks over their faces. ",
    activated_card:true },
    { id:'',
    name: "Nyan Cat",
    image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
    energy:100,
    cost:500,
    card_type:"UR",
    card_race:"Nyan",
    description: "Nyanyanyanyanyanyanya!",
    activated_card:true},
    { id:'',
    name: "Mametchi",
    image: "https://tamagotchi.com/wp-content/uploads/mametchi.jpg",
    energy:30,
    cost:500,
    card_type:"SSR",
    card_race:"Tamagotchi",
    description: "Mametchi loves inventing things and though sometimes he fails he will succeed, he just keeps trying. He loves to study and play sports.",
    activated_card:true },
    { id:'',
    name: "Ginjirotchi",
    image:"https://tamagotchi.com/wp-content/uploads/ginjirotchi.jpg",
    energy:100,
    cost:500,
    card_type:"UR",
    card_race:"Nyan",
    description: "Ginjirotchi is cheerful and full of energy, though also compassionate. He loves watching dramatic movies.",
    activated_card:true},
    { id:'', 
    name: "Korok",
    image:"https://static.wikia.nocookie.net/zelda_gamepedia_en/images/b/b0/TWW_Makar_Artwork.png",
    energy:100,
    cost:5000,
    card_type:"MR",
    card_race:"Korok",
    description: "The Koroks are a race in The Legend of Zelda series. They are small, wooden people who wear leaf masks over their faces. ",
    activated_card:true },
    { id:'',
    name: "Nyan Cat",
    image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
    energy:100,
    cost:500,
    card_type:"UR",
    card_race:"Nyan",
    description: "Nyanyanyanyanyanyanya!",
    activated_card:true},
    { id:'',
    name: "Mametchi",
    image: "https://tamagotchi.com/wp-content/uploads/mametchi.jpg",
    energy:30,
    cost:500,
    card_type:"SSR",
    card_race:"Tamagotchi",
    description: "Mametchi loves inventing things and though sometimes he fails he will succeed, he just keeps trying. He loves to study and play sports.",
    activated_card:true },
    { id:'',
    name: "Ginjirotchi",
    image:"https://tamagotchi.com/wp-content/uploads/ginjirotchi.jpg",
    energy:100,
    cost:500,
    card_type:"UR",
    card_race:"Nyan",
    description: "Ginjirotchi is cheerful and full of energy, though also compassionate. He loves watching dramatic movies.",
    activated_card:true},
    { id:'', 
    name: "Korok",
    image:"https://static.wikia.nocookie.net/zelda_gamepedia_en/images/b/b0/TWW_Makar_Artwork.png",
    energy:100,
    cost:5000,
    card_type:"MR",
    card_race:"Korok",
    description: "The Koroks are a race in The Legend of Zelda series. They are small, wooden people who wear leaf masks over their faces. ",
    activated_card:true },
    {id:'',
    name: "Mametchi",
    image: "https://tamagotchi.com/wp-content/uploads/mametchi.jpg",
    energy:30,
    cost:500,
    card_type:"SSR",
    card_race:"Tamagotchi",
    description: "Mametchi loves inventing things and though sometimes he fails he will succeed, he just keeps trying. He loves to study and play sports.",
    activated_card:true },
    { id:'',
    name: "Ginjirotchi",
    image:"https://tamagotchi.com/wp-content/uploads/ginjirotchi.jpg",
    energy:100,
    cost:500,
    card_type:"UR",
    card_race:"Nyan",
    description: "Ginjirotchi is cheerful and full of energy, though also compassionate. He loves watching dramatic movies.",
    activated_card:true},
    { id:'', 
    name: "Korok",
    image:"https://static.wikia.nocookie.net/zelda_gamepedia_en/images/b/b0/TWW_Makar_Artwork.png",
    energy:100,
    cost:5000,
    card_type:"MR",
    card_race:"Korok",
    description: "The Koroks are a race in The Legend of Zelda series. They are small, wooden people who wear leaf masks over their faces. ",
    activated_card:true }];
   
     */
    
  
    

}
