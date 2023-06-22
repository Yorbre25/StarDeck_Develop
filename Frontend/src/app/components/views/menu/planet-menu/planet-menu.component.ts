import { Component, OnInit } from '@angular/core';
import { planetService } from 'src/app/components/services/Planet.service';
import { PlanetInterface } from 'src/app/components/interfaces/planet.interface';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-planet-menu',
  templateUrl: './planet-menu.component.html',
  styleUrls: ['./planet-menu.component.scss']
})
export class PlanetMenuComponent implements OnInit {
  planets!: PlanetInterface[];

  constructor(private planetService: planetService, private http: HttpClient) {

  }

  ngOnInit(): void {
    this.planetService.getAllPlanets().subscribe(data => {
      console.log(data)
      this.planets = data 
    });
  }
}