import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.scss']
})
export class LobbyComponent{
  constructor(private router: Router) {}
  
  ngOnInIt(): void {

  }
  
  findGame(){
    const uuid = uuidv4();
    console.log(uuid);
    this.router.navigate(['/partida', uuid]);

  }

}
