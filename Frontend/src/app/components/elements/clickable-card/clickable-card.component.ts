import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-clickable-card',
  templateUrl: './clickable-card.component.html',
  styleUrls: ['./clickable-card.component.scss']
})
export class ClickableCardComponent {

  @Input()
  name!: string;
  @Input()
  image!: string;
  @Input()
  energy!: string;
  @Input()
  price!: string;
  @Input()
  type!: string;
  @Input()
  race!: string;
  @Input()
  description!: string;

  constructor(private router: Router) {
   
  }


  onClick(){

    this.router.navigate(['/cards']);
    

  }
}
