import { Component, OnInit } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';
import { Router } from '@angular/router';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { DeckInterface } from '../../interfaces/deck.interface';

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

  //Variables del backend
  deck !: DeckInterface;
  currentCards!: number;
  name !: string[];
  fault!: boolean;

  deckName = new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(30)]);
  card = new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(30)]);

  // totalCards = new FormArray([]);

  constructor(private router: Router, private _formBuilder: FormBuilder, private api: ApiService) {
    
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
    }}

    ngOnInit() {
      this.currentCards = 0;
      this.fault = false
    }


    goToLobby() {
      if (this.deckName.value != null) {
        if (this.deckName.invalid) {
          this.fault = true
        } else if (this.deckName.value != null) {
          this.deck.name = this.deckName.value
         // this.deck.cards = this.totalCards.value
          
    
    
         // this.api.addCard(this.card).subscribe(data => {
           // console.log(data);
         // })//acá llama a la API
    
          this.router.navigate(['/home']);
    
        }
      }
    }
    
  addCardToTotalCards() {
    if (!(this.currentCards > 18)) {
      //this.totalCards.push(new FormControl(''));
      this.currentCards++;
    }
  }

}


