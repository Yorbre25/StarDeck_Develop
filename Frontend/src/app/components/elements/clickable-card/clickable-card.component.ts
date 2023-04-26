import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import {CardInt} from '../../interfaces/card.interface';

@Component({
  selector: 'app-clickable-card',
  templateUrl: './clickable-card.component.html',
  styleUrls: ['./clickable-card.component.scss']
})
export class ClickableCardComponent {

  @Input()
  element!: {
    id: string|null; 
    name: string|null; 
    image: string|null; 
    description: string|null; 
    energy: string|null; 
    cost: number|undefined; 
    card_type: string|null; 
    card_race: string|null; 
    activated_card: boolean|null};

  constructor(private router: Router, private api:ApiService) {
   
  }


  onClick(){
    
    this.api.addCard(this.element);
    this.router.navigate(['/cards']);

    

  }
}
