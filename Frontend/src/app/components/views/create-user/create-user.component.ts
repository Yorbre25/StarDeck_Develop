import { Component } from '@angular/core';
import { Router, NavigationExtras } from '@angular/router';

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
