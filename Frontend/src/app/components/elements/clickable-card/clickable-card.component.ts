import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';

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
    energy: number|undefined; 
    cost: number|undefined; 
    type: string|null; 
    race: string|null;
  };

  constructor(private router: Router, private logins:LoginService) {
   
  }


  onClick(){
    
    //this.api.playerchoseCard(this.element,this.logins.getcorreo());
    console.log("CLICKABLE CARD LOG")
    this.router.navigate(['/cards']);

  }
}
