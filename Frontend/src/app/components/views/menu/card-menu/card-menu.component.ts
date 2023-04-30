import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/components/services/api.service';
import { CardInt } from 'src/app/components/interfaces/card.interface';
import { HttpClient } from '@angular/common/http';

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
export class CardMenuComponent implements OnInit {
  cards!: CardInt[];

  constructor(private api: ApiService, private http: HttpClient) {

    //console.log(this.cards)
  }

  ngOnInit(): void {
    //  this.api.getAllCards().subscribe(data => {
    //  console.log(data)
    // this.cards = data 
    // });
    this.http.get('assets/samples/sampleCards.json').subscribe((data: any) => {
      console.log(data);
      this.cards = data
    });
}

}

