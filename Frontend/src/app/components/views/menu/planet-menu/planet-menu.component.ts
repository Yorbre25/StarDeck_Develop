import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/components/services/api.service';
import { Planet } from 'src/app/components/interfaces/planet.interface';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-planet-menu',
  templateUrl: './planet-menu.component.html',
  styleUrls: ['./planet-menu.component.scss']
})
export class PlanetMenuComponent implements OnInit {
  planets!: Planet[];

  constructor(private api: ApiService, private http: HttpClient) {

    //console.log(this.cards)
  }

  ngOnInit(): void {
    //  this.api.getAllCards().subscribe(data => {
    //  console.log(data)
    // this.cards = data 
    // });
    this.http.get('assets/samples/samplePlanets.json').subscribe((data: any) => {
      console.log(data);
      this.planets = data
    });
  }
}