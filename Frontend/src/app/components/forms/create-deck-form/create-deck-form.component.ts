import { Component, OnInit } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';
import { Router } from '@angular/router';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { seleced_Card_S } from '../../services/selected_card.service';
import { LoginService } from '../../services/login.service';
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
  amountFault!:boolean;

  deckName = new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(30)]);
  card = new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(30)]);

  // totalCards = new FormArray([]);

  constructor(private router: Router, private _formBuilder: FormBuilder, private api: ApiService, private http: HttpClient, public dialog: MatDialog, private scard:seleced_Card_S, private logs:LoginService) {

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
      }
      else {
        return ""
      }
    } else {
      return "Esto no debería de pasar"
    }
  }

  ngOnInit() {
    this.scard.resetCardList()
    this.currentCards = 0;
    this.fault = false
    this.totalCards = 5;
    this.amountFault=false;
    this.deck={id:"",
               name:"",
              cards:[]}

    this.http.get('assets/samples/sampleCards.json').subscribe((data: any) => {
      console.log(data);
      this.allCards = data
    });

    //this.api.getAllCards().subscribe(data => {
      //console.log(data)
      // this.allCards = data //aqui hay que hacer que traiga solo las del usuario
    //});

  }

  goToLobby() {
    if (this.deckName.value != null) {
      if (this.deckName.invalid) {
        this.fault = true
      } else{
        this.deck.name = this.deckName.value
        this.deck.cards=this.scard.getCardList()
        this.deck.id=this.logs.getid()  
        this.currentCards=this.deck.cards.length
        if (this.currentCards==5){
          //Aquí se haría el post al api
          this.router.navigate(['/decks']);}
        else{
          console.log("Missing Cards")
        }
      }
    }
  }

  addCardToTotalCards() {
    if (!(this.currentCards > 18)) {
      //this.totalCards.push(new FormControl(''));
      this.currentCards++;
    }
  }

  openAllCards(cards: CardInt[]) {
    console.log(cards);
    const dialogRef = this.dialog.open(MultipleCardsComponent, {
      data: { cards: this.allCards, clickable: true}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

}



