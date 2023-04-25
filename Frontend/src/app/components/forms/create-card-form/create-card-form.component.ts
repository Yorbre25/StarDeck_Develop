import { Component, OnInit } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';
import { Router } from '@angular/router'; 
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import { ApiService } from '../../services/api.service';


@Component({
  selector: 'app-create-card-form',
  templateUrl: './create-card-form.component.html',
  styleUrls: ['./create-card-form.component.scss']
})
export class CreateCardFormComponent {
  //Variables del backend
  name !: CardInt;
  types !: string[];
  fault!:boolean;


  //Objetos de input del frontend
  characterName = new FormControl('',[Validators.required]);
  description = new FormControl('',[Validators.required]);
  race = new FormControl('',[Validators.required]);
  energy = new FormControl('',[Validators.required]);
  price = new FormControl('',[Validators.required]);
  type = new FormControl('',[Validators.required]);


  constructor(private router: Router, private _formBuilder: FormBuilder,private api:ApiService) {}
  
  //La idea es que este módul genera el mensaje de error
  getErrMessage(component:FormControl){
    if(component.hasError('required')){//El usuario no escribió nada
      return "Este campo es obligatorio."
    }else if(component.value.length>30){//La contraseña no cuenta con la longitud requerida
      return "Las contraseña debe tener un tamaño de 8 caracteres"
    }else if(component.invalid){//El usuario se pasa de la cantidad de caracteres
      return "El usuario debe tener entre 1 y 30 caracteres"}
    else{
      return ""
    }
  }
  
  goToLobby(){

    this.router.navigate(['/lobby']);

  }
}
