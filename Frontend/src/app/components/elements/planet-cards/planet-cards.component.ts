
import { Component , Input, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { selected_Card_S } from '../../services/selected_card.service';
import { CardInt } from 'src/app/components/interfaces/card.interface';
import { HttpClient } from '@angular/common/http';
import { PlanetInterface } from 'src/app/components/interfaces/planet.interface';

@Component({
  selector: 'app-planet-cards',
  templateUrl: './planet-cards.component.html',
  styleUrls: ['./planet-cards.component.scss']
})
export class PlanetCardsComponent {

  
  
  //planet!: PlanetInterface;

  hidden!:PlanetInterface;
  isSelected!: boolean | null;

  @Input()
  planet!:PlanetInterface;

  @Input()
  cards!: CardInt[];
  @Input()
  opponentCards!: CardInt[];
  @Input()
  currentUserPoints!: number;
  @Input()
  opponentPoints!: number;
  @Input()
  opponentName!: string|null;
  @Input()
  currentUserName!: string|null;

  cardToAdd!: CardInt; 


  @Input()
  canSelect!: boolean | null; 

  @Input()
  index!: number; 

  @Output() planetClicked: EventEmitter<number> = new EventEmitter<number>();



  constructor(private http: HttpClient) {

    //console.log(this.cards)
  }


  ngOnInit(): void {
    this.http.get('assets/game/hiddenPlanet.json').subscribe((data3: any) => {
      this.hidden = data3
    });
}

onClick() {
  this.toggleSelection();
  
  //if (this.clickable) {
    //if(this.element.id==''){
      //console.log("Card not ready yet")
    //}else{
      //this.Scard.setcard(this.element)
    //}
  //}
  
}

toggleSelection() {
  this.isSelected = !this.isSelected;
}


onPlanetClick() {
  // Emit the planet index or any other relevant information
  this.planetClicked.emit(this.index);
  console.log("ayuda" + this.index);

}

}


