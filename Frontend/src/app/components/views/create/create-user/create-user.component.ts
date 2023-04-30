import { Component } from '@angular/core';
import { Router, NavigationExtras } from '@angular/router';

/**
 * @description 
 * This component acts as a view for the user registration. 
 * 
 * @typedef {class} CreateUserComponent

 * @property {Function} goToLobby - The function routes to the lobby. 

*/
@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})


export class CreateUserComponent {


  constructor(private router: Router) { }




  ngOnInIt(): void {

  }
  goToLobby() {

    this.router.navigate(['/home']);

  }
}
