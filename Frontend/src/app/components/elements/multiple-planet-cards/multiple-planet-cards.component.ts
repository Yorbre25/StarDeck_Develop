import { Component, Input, OnInit} from '@angular/core';
import { PlanetInterface } from '../../interfaces/planet.interface';
import { HttpClient } from '@angular/common/http';
import { ApiService } from 'src/app/components/services/api.service';

@Component({
  selector: 'app-multiple-planet-cards',
  templateUrl: './multiple-planet-cards.component.html',
  styleUrls: ['./multiple-planet-cards.component.scss']
})
export class MultiplePlanetCardsComponent {
  @Input()
  planets!:PlanetInterface[];

  constructor(private api: ApiService, private http: HttpClient) {

    //console.log(this.cards)
  }

  ngOnInit(): void {
    this.http.get('assets/samples/samplePlanets.json').subscribe((data: any) => {
      console.log(data);
      this.planets = data
    });
  }
}
