import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/components/services/api.service';
import { PlanetInterface } from 'src/app/components/interfaces/planet.interface';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-planet-menu',
  templateUrl: './planet-menu.component.html',
  styleUrls: ['./planet-menu.component.scss']
})
export class PlanetMenuComponent implements OnInit {
  planets!: PlanetInterface[];

  constructor(private api: ApiService, private http: HttpClient) {

  }

  ngOnInit(): void {
    this.api.getAllPlanets().subscribe(data => {
      console.log(data)
      this.planets = data 
    });
  }
}