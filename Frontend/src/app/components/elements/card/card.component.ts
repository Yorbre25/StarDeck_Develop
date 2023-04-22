import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent {

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
}
