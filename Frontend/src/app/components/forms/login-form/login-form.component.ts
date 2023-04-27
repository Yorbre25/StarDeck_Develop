import { Component, NgModule } from '@angular/core';
import { Router } from '@angular/router';
import {FormBuilder, FormControl} from '@angular/forms';
import {FloatLabelType} from '@angular/material/form-field';
import { LoginService } from '../../services/login.service';


/**
 *@description This component acts as a user register form.
*/


@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']

})

export class LoginFormComponent {
  hideRequiredControl = new FormControl(false);
  floatLabelControl = new FormControl('auto' as FloatLabelType);
  options = this._formBuilder.group({
    hideRequired: this.hideRequiredControl,
    floatLabel: this.floatLabelControl,
  });

  constructor(private router: Router, private _formBuilder: FormBuilder, private logins:LoginService) {}
  
  getFloatLabelValue(): FloatLabelType {
    return this.floatLabelControl.value || 'auto';
  }

  goToLobby(){


    this.logins.setcorreo("nasserbrwn%40gmail.com")
    this.logins.setid("U-ah2s7fvkzrhn")
    this.router.navigate(['/home']);


  }

}
