import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatDialogActions } from '@angular/material/dialog';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { ApiService } from '../../services/api.service';
import { CardInt } from '../../interfaces/card.interface';
import { LoginService } from '../../services/login.service';
import { selected_Card_S } from '../../services/selected_card.service';
import { RouterTestingHarness } from '@angular/router/testing';
import { FactoryTarget } from '@angular/compiler';

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

  cardselectedfault:boolean=false;
  cardsPack = 0;
  emptycard:CardInt={ 
  id:'',
  name: "Loading Card...",
  image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
  energy:100,
  cost:100,
  type:"Loading...",
  race:"Loading...",
  description: "Loading description..."
}
  cards!:CardInt[];
  cardsAmount!:number;
  clickableCardsPack!:CardInt[][];
  clickableCards: CardInt[] = [
    { id:'',
    name: "Loading Card...",
    image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
    energy:100,
    cost:100,
    type:"Loading...",
    race:"Loading...",
    description: "Loading description..."
  },
  { id:'',
  name: "Loading Card...",
  image: "https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
  energy:100,
  cost:100,
  type:"Loading...",
  race:"Loading...",
  description: "Loading description..."
  },
  { id:'',
  name: "Loading Card...",
  image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
  energy:100,
  cost:100,
  type:"Loading...",
  race:"Loading...",
  description: "Loading description..."
  }]



  constructor(private dialogRef: MatDialogRef<InitialCardChooserComponent>, private api: ApiService, private logins:LoginService,private Scard:selected_Card_S) {}
  
  ngOnInit(): void {
    this.Scard.setcard(this.emptycard)
    this.api.getplayerCards(this.logins.getid()).subscribe(data => {
      console.log("Player cards")
      console.log(data)
      this.cards = data 
    });
    this.api.getchoosingcard().subscribe(data=>{
      console.log("Cards to choose")
      console.log(data)
      this.clickableCardsPack=data
      this.clickableCards=data[0]
    })
    this.api.getAmCards(this.logins.getid()).subscribe(data=>{
      this.cardsAmount=data
    })
    
  }

  close(){
    if(this.Scard.getcard()!=''){
      this.cardselectedfault=false
      this.cardsPack+=1
      console.log(this.cardsAmount)  
      if (this.cardsAmount==18){
        this.dialogRef.close();  
      }else{
        this.api.playerchoseCard(this?.Scard.getcard(),this.logins.getid()).subscribe((response)=>{
          this.cardsAmount+=1
          console.log("The response:")
          console.log(response)
          this.api.getplayerCards(this.logins.getid()).subscribe(data => {//Actualizo las cartas del jugador
            console.log(data)
            this.cards = data 
            this.clickableCards=this.clickableCardsPack[this.cardsPack]
            this.Scard.setcard(this.emptycard)
            if (this.cardsAmount==18){
              this.dialogRef.close();  
            }
          });
        })//Link de la carta que el jugador eligió
      }
    }else{
      this.cardselectedfault=true
      console.log('User did not select a card')
    }
  }
}

