import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { CardInt } from '../../interfaces/card.interface';
import { LoginService } from '../../services/login.service';
import { seleced_Card_S } from '../../services/selected_card.service';
/**
 * @description
 * This component displays content belonging to an existing card from 
 * the database inside a rectangular linear-gradient colored block. 
 * The content displayed includes: name, description, and character image, energy, cost, 
 * type and race. 
*/
@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent {


  @Input()
  element!: {
    id: string | null;
    name: string | null;
    image: string | null;
    description: string | null;
    energy: number | undefined;
    cost: number | undefined;
    type: string | null;
    race: string | null;
  };

  @Input() clickable: boolean = false;


  constructor(private router: Router, private api: ApiService, private logins:LoginService, private Scard:seleced_Card_S) {
  }


  onClick() {
    if (this.clickable) {
      if(this.element.id==''){
        console.log("Card not ready yet")
      }else{
        this.Scard.setcard(this.element)
      }
    }


  }
}