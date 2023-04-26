import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-clickable-card',
  templateUrl: './clickable-card.component.html',
  styleUrls: ['./clickable-card.component.scss']
})
export class ClickableCardComponent {

  @Input()
  element!: { name: string|null; 
    image: string|null; 
    description: string|null; 
    energy: string|null; 
    price: string|null; 
    type: string|null; 
    race: string|null; };

  constructor(private router: Router) {
   
  }


  onClick(){

    this.router.navigate(['/cards']);
    

  }
}
