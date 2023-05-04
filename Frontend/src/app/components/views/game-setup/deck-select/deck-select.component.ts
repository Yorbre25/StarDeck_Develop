import { Component } from '@angular/core';
import { ApiService } from 'src/app/components/services/api.service';
import { DeckInterface } from 'src/app/components/interfaces/deck.interface';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { SingleDeckComponent } from 'src/app/components/views/single-deck/single-deck.component';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
@Component({
  selector: 'app-deck-select',
  templateUrl: './deck-select.component.html',
  styleUrls: ['./deck-select.component.scss']
})
export class DeckSelectComponent {

  decks!: DeckInterface[];

  constructor(private router: Router, private api: ApiService, private http: HttpClient, public dialog: MatDialog) {

    //console.log(this.cards)
  }


  ngOnInit(): void {
    //  this.api.getAllCards().subscribe(data => {
    //  console.log(data)
    // this.cards = data 
    // });
    this.http.get('assets/samples/sampleDecks.json').subscribe((data: any) => {
      console.log(data);
      this.decks = data
    });
  }

  deckSelected(deck: DeckInterface){
    /**
     *  const dialogRef = this.dialog.open(SingleDeckComponent, {
      data: { deck }
    });
  
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });

    
     */

    const uuid = uuidv4();
    console.log(uuid);
    this.router.navigate(['/match', uuid]);
   
  }
}