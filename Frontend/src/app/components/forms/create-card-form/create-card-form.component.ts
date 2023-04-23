import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-card-form',
  templateUrl: './create-card-form.component.html',
  styleUrls: ['./create-card-form.component.scss']
})
export class CreateCardFormComponent {

  constructor(private router: Router) {}
  goToLobby(){

    this.router.navigate(['/lobby']);

  }
}
