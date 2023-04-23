import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {FormBuilder, FormControl} from '@angular/forms';

@Component({
  selector: 'app-sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['./sign-up-form.component.scss']
})
export class SignUpFormComponent {

  constructor(private router: Router, private _formBuilder: FormBuilder) {}
  goToLobby(){

    this.router.navigate(['/lobby']);

  }

}
