import { Component } from '@angular/core';
import { CardInt } from 'src/app/components/interfaces/card.interface';
import { HttpClient } from '@angular/common/http';
import { ApiService } from 'src/app/components/services/api.service';
import { PlanetInterface } from 'src/app/components/interfaces/planet.interface';
@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent {

  cards!: CardInt[];
  planets!: PlanetInterface[];


  constructor(private api: ApiService, private http: HttpClient) {

    //console.log(this.cards)
  }

  ngOnInit(): void {
    //  this.api.getAllCards().subscribe(data => {
    //  console.log(data)
    // this.cards = data 
    // });
    this.http.get('assets/samples/sampleCards.json').subscribe((data: any) => {
      console.log(data);
      this.cards = data
    });

    this.http.get('assets/samples/samplePlanets.json').subscribe((data2: any) => {
      console.log(data2);
      this.planets = data2
    });
}
}