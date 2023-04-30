import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DeckInterface } from '../../interfaces/deck.interface';
import { CardInt } from '../../interfaces/card.interface';
import { ApiService } from '../../services/api.service';
import { HttpClient } from '@angular/common/http';
import { MatCardContent } from '@angular/material/card';
@Component({
  selector: 'app-single-deck',
  templateUrl: './single-deck.component.html',
  styleUrls: ['./single-deck.component.scss']
})
export class SingleDeckComponent {


  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { 
    console.log(data)
    console.log(this.data.deck.name)
  }
}
