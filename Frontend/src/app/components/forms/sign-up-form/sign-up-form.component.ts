import { Component, OnInit } from '@angular/core';
import { AccountInt } from '../../interfaces/account.interface';
import { Router } from '@angular/router'; 
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import { ApiService } from '../../services/api.service';


@Component({
  selector: 'app-sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['./sign-up-form.component.scss']
})

export class SignUpFormComponent implements OnInit {
  //Variables del backend
  user !: AccountInt;
  nationalities !: string[];
  fault!:boolean;


  //Objetos de input del frontend
  mail = new FormControl('',[Validators.required,Validators.email]);
  playerName = new FormControl('',[Validators.required]);
  playerLastName = new FormControl('',[Validators.required]);
  playerNationality = new FormControl('',[Validators.required]);
  playerAlias = new FormControl('',[Validators.required,Validators.maxLength(30)]);
  playerPassword = new FormControl('',[Validators.required]);
  confirmPassword = new FormControl('',[Validators.required]);

  
  
  constructor(private router: Router, private _formBuilder: FormBuilder,private api:ApiService) {}

  //La idea es que este módul genera el mensaje de error
  getErrMessage(component:FormControl){
    if(component.hasError('required')){//El usuario no escribió nada
      return "Este campo es obligatorio"
    }else if(component.hasError('email')){//El mail no es en formato válido
      return "Ingrese un mail valido"
    }else if(this.playerPassword.value!=this.confirmPassword.value){//Las contraseñas no coinciden
      return "Las contraseñas deben coincidir"
    }else if(this.playerPassword.value?.length!=8){//La contraseña no cuenta con la longitud requerida
      return "Las contraseña debe tener un tamaño de 8 caracteres"
    }else if(component.invalid){//El usuario se pasa de la cantidad de caracteres
      return "El usuario debe tener entre 1 y 30 caracteres"}
    else{
      return ""
    }
  }
  //Valida la información y guardará en caso que todo esté correcto
  goToLobby(){
    if(this.mail.invalid||this.playerName.invalid||this.playerLastName.invalid||this.playerNationality.invalid||this.playerAlias.invalid||this.playerPassword.invalid){
      this.fault=true
    }else if(this.playerPassword.value!=this.confirmPassword.value||this.playerPassword.value?.length!=8){
      this.fault=true
    }else{
      this.user.mail = this.mail.value
      this.user.playerName = this.playerName.value
      this.user.playerLastName = this.playerLastName.value
      this.user.playerNationality = this.playerNationality.value
      this.user.playerAlias = this.playerAlias.value
      this.user.playerPassword=this.playerPassword.value

      this.api.registerAccount(this.user)//acá llama a la API
      this.router.navigate(['/lobby']);
      }
  }


  //Inicializa las variables
  ngOnInit(){
    this.user = {
      mail:'',
      playerName:'',
      playerLastName:'',
      playerNationality:'',
      playerAlias:'',
      playerPassword:''
    }
    this.nationalities=["Estados Unidos","México","Costa Rica"]

    this.fault=false
  }

}
