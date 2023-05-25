import { Component, OnInit } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { deckService } from '../../services/deck.service';
import { CardService } from '../../services/Card.service';
import { LoginService } from '../../services/login.service';
import { selected_Card_S } from '../../services/selected_card.service';
import { DeckInterface } from '../../interfaces/deck.interface';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { SingleDeckComponent } from 'src/app/components/views/single-deck/single-deck.component';
import { MultipleCardsComponent } from '../../elements/multiple-cards/multiple-cards.component';

/**
 * @description
 * This component acts as a user register form for deck creation. The required fields are:
 * deck name, cards to be added.
 * 
 * @typedef {class} CreateDeckFormComponent
 * 
 * @property {DeckInterface} deck- deck to be created. 
 * @property {string[]} cards - cards in deck. 
 * @property {string} deckName - deck name.  
 * 
 * 
 * @property {Function} onSubmit - The function to call when the form is created.
 * @property {Function} goToLobby - The function to call when the form is submitted.
*/

@Component({
  selector: 'app-create-deck-form',
  templateUrl: './create-deck-form.component.html',
  styleUrls: ['./create-deck-form.component.scss']
})
export class CreateDeckFormComponent {

  decks!: DeckInterface[];
  allCards!: CardInt[];

  //Variables del backend
  deck !: DeckInterface;
  currentCards!: number;
  totalCards!: number;
  name !: string[];
  fault!: boolean;
  deckNameFault!: boolean;

  deckName = new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]);
  card = new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(30)]);

  // totalCards = new FormArray([]);

  constructor(private router: Router, private _formBuilder: FormBuilder, private deckService: deckService, private http: HttpClient, public dialog: MatDialog, private LoginS:LoginService, private SCard:selected_Card_S, private cardService:CardService) {

  }

  //La idea es que este módul genera el mensaje de error
  getErrMessage(component: FormControl) {
    if (this.deckName.value != null) {
      if (component.hasError('required')) {//El usuario no escribió nada
        return "Este campo es obligatorio."
      } else if (component.hasError('minlength')) {
        return "No se cumple con el número mínimo de caracteres"
      } else if (component.hasError('maxlength')) {
        return "Se ha excedido el número de caracteres."
      } else if (this.deckNameFault){
        return "Usted ya tiene un deck con este nombre"
      }else {
        return ""
      }
    } else {
      return "Esto no debería de pasar"
    }
  }

  ngOnInit() {
    this.SCard.initializeCardList()
    this.SCard.cardList$.subscribe((value: string[]) => {
      this.currentCards = value.length
    })
    this.fault = false;
    this.deckNameFault = false;
    this.totalCards = 18;
    this.deck = {
      "id": "",
      "name": ""
    }
    
    
    this.http.get('assets/samples/sampleCards.json').subscribe((data: any) => {
      console.log(data);
      this.allCards = data
    });
    

    this.cardService.getplayerCards(this.LoginS.getid()).subscribe(data => {
      console.log(data)
      this.allCards = data 
    });


  }

    goToLobby() {
      if (this.deckName.value != null) {
        if (this.deckName.invalid) {
          this.fault = true
        }else{
          console.log(this.SCard.getcardList())
          if(this.currentCards==this.totalCards){
            this.deck.name=this.deckName.value
            this.deckService.addDeck(this.LoginS.getid(),this.deck.name,this.SCard.getcardList()).subscribe((response)=>{
              this.router.navigate(['/decks']);
            },(error)=>{
              console.log(error)
              this.deckNameFault=true
            });
          }          
         }
      }
  }



  openAllCards(cards: CardInt[]) {
    console.log(cards);
    const dialogRef = this.dialog.open(MultipleCardsComponent, {
      data: { cards: this.allCards, clickable: true }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

}



