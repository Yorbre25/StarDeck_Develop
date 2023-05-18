import { Component, Inject, OnInit } from '@angular/core';
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
export class SingleDeckComponent implements OnInit {
  DeckCards!:CardInt[];

  constructor(@Inject(MAT_DIALOG_DATA) public something: any, private api:ApiService) { 
    api.getDeckCards(this.something.deck.id).subscribe((data)=>{
        this.DeckCards=data
    })
  }

  ngOnInit(): void {
    
  }
}
