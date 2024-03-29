import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatDialogActions } from '@angular/material/dialog';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { ApiService } from '../../services/api.service';
import { CardInt } from '../../interfaces/card.interface';
import { LoginService } from '../../services/login.service';
import { seleced_Card_S } from '../../services/selected_card.service';

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

  clickableCards: CardInt[] = [
    
    { id:'',
    name: "Nyan Cat",
    image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
    energy:100,
    cost:500,
    type:"UR",
    race:"Nyan",
    description: "Nyanyanyanyanyanyanya!",
    activated_card:true}, 
  { id:'',
  name: "Mametchi",
  image: "https://tamagotchi.com/wp-content/uploads/mametchi.jpg",
  energy:30,
  cost:500,
  type:"SSR",
  race:"Tamagotchi",
  description: "Mametchi loves inventing things and though sometimes he fails he will succeed, he just keeps trying. He loves to study and play sports.",
  activated_card:true },
  { id:'',
  name: "Ginjirotchi",
  image:"https://tamagotchi.com/wp-content/uploads/ginjirotchi.jpg",
  energy:100,
  cost:500,
  type:"UR",
  race:"Nyan",
  description: "Ginjirotchi is cheerful and full of energy, though also compassionate. He loves watching dramatic movies.",
  activated_card:true}]

 // clickableCards!:CardInt[];


    constructor(private dialogRef: MatDialogRef<InitialCardChooserComponent>, private api: ApiService, private logins:LoginService,private Scard:seleced_Card_S) {
      
      //console.log(this.cards)
    }
    ngOnInit(): void {
      this.api.getplayerCards(this.logins.getcorreo()).subscribe(data => {
        console.log(data)
        this.cards = data 
      });
      this.close()  
      

    }

    close() {
      if (this.cardsChosen==2){
        this.dialogRef.close();}
      else{
        this.cardsChosen+=1
        this.api.playerchoseCard(this?.Scard.getcard(),this.logins.getid())
        this.api.getplayerCards(this.logins.getcorreo()).subscribe(data => {
          console.log(data)
          this.cards = data 
        });
        this.api.getchoosingcard(this.logins.getid()).subscribe(data=>{
          this.clickableCards=data
        })
      }
    }
    
    
  
    

}
