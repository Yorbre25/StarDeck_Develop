import { Component } from '@angular/core';
import { DeckInterface } from 'src/app/components/interfaces/deck.interface';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { SingleDeckComponent } from 'src/app/components/views/single-deck/single-deck.component';
import { LoginService } from 'src/app/components/services/login.service';
import { deckService } from 'src/app/components/services/deck.service';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
@Component({
  selector: 'app-deck-select',
  templateUrl: './deck-select.component.html',
  styleUrls: ['./deck-select.component.scss']
})
export class DeckSelectComponent {

  decks!: DeckInterface[];

  constructor(private router: Router, private http: HttpClient, public dialog: MatDialog, private deckService:deckService, private loginService:LoginService) {}


  ngOnInit(): void {
    this.deckService.getAllDecks(this.loginService.getid()).subscribe(data => {
      console.log(data)
      this.decks = data 
     });
    /** 
    this.http.get('assets/samples/sampleDecks.json').subscribe((data: any) => {
      console.log(data);
      this.decks = data
    });
    */
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
    this.router.navigate(['/searching']);   
  }
}