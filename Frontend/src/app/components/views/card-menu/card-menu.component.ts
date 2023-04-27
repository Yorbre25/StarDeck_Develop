import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { CardInt } from '../../interfaces/card.interface';

/**
 * @description 
 * This component acts as a view for the user cards. 
 * 
 * @typedef {class} CardMenuComponent
 * 
 * @property {CardInt[]} cards - cards to be displayed. 
 * 
 * @property {Function} ngOnInIt - The function to call when the form is created calls the api service to retrieve cards.

*/

@Component({
  selector: 'app-card-menu',
  templateUrl: './card-menu.component.html',
  styleUrls: ['./card-menu.component.scss']
})
export class CardMenuComponent implements OnInit{
  cards!:CardInt[];

  constructor(private api: ApiService) {
      
    //console.log(this.cards)
  }

  ngOnInit(): void {
    this.api.getAllCards().subscribe(data => {
      console.log(data)
      this.cards = data 
    });
  }
}
