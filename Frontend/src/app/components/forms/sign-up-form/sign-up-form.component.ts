import { Component, OnInit } from '@angular/core';
import { AccountInt } from '../../interfaces/account.interface';
import { Router } from '@angular/router'; 
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { InitialCardChooserComponent } from '../../pop-ups/initial-card-chooser/initial-card-chooser.component';
import { LoginService } from '../../services/login.service';

/**
 * @description
 * This component acts as a user register form for user creation. The required fields are:
 * mail, player name, player last name, player nationality, player alias, player password, player password confirmation.
 * 
 * @typedef {class} CreateCardFormComponent
 * 
 * @property {AccountInt} user- user to be created. 
 * @property {string[]} nationalities - array of available nationalities. 
 * @property {boolean} fault - Indicates if there is an error in the user inputs.  
 * 
 * @property {string} mail - user email. 
 * @property {string} playerName - user name. 
 * @property {string} playerLastName - user last name. 
 * @property {string} playerNationality- user nationality. 
 * @property {string} playerAlias - user alias.  
 * @property {string} playerPassword - user password. 
 * @property {string} confirmPassword - user password confirmation. 
 * 
 * @property {Function} ngOnInIn - The function to call when the form is created instantias a user with lvl 0 and 20 coins
 * @property {Function} goToLobby - The function to call when the form is submitted.
 * @property {Function} getErrMessage - The function to call when fault is true. 
*/


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
  emailalreadytaken!:boolean;



  //Objetos de input del frontend
  mail = new FormControl('',[Validators.required,Validators.email]);
  playerName = new FormControl('',[Validators.required]);
  playerLastName = new FormControl('',[Validators.required]);
  playerNationality = new FormControl('',[Validators.required]);
  playerAlias = new FormControl('',[Validators.required,Validators.maxLength(30)]);
  playerPassword = new FormControl('',[Validators.required]);
  confirmPassword = new FormControl('',[Validators.required]);

  
  
  constructor(private router: Router, private _formBuilder: FormBuilder,private api:ApiService, private logs:LoginService) {}

  //La idea es que este módulo genera el mensaje de error
  getErrMessage(component:FormControl){
    if(component.hasError('required')){//El usuario no escribió nada
      return "Este campo es obligatorio"
    }else if(component.hasError('email')){
      return "Ingrese un mail valido"
    }else if(component.hasError('maxlength')){
      return "El usuario debe tener entre 1 y 30 caracteres"
    }else if(this.emailalreadytaken){
      return ("El correo proporcionado ya se encuentra registrado")
    }else if(this.playerPassword.value!=this.confirmPassword.value){
      return "Las contraseñas deben coincidir"
    }else if(this.playerPassword.value?.length!=8){
      return "Las contraseña debe tener un tamaño de 8 caracteres"
    }else{
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
      this.user.email = this.mail.value
      this.user.f_name = this.playerName.value
      this.user.l_name = this.playerLastName.value
      this.user.country = this.playerNationality.value
      this.user.nickname = this.playerAlias.value
      this.user.p_hash=this.playerPassword.value

      this.api.registerAccount(this.user).subscribe(data=>{//acá llama a la API
        console.log(data);
        if(data.status == "ok"){
          if(this.user.email!=null && this.user.id!=null){
            this.api.getPlayerInfo(this.user.email).subscribe(data=>{
              this.user=data
            })
            this.logs.setcorreo(this.user.email) //Guarda el correo del usuario que está actualmente loggeado
            this.logs.setid(this.user.id) //Guarda el id del usuario que está actualmente loggeado
          }
          this.router.navigate(['/home']);
        }else{
          this.emailalreadytaken=true
        }

      })
      }
     
   
  }


  //Inicializa las variables
  ngOnInit(){
    this.user = {
      id:'',
      email:'',
      f_name:'',
      l_name:'',
      country:'',
      nickname:'',
      p_hash:'',
      ranking:'',
      lvl:0,
      coins:20,//
      avatar:'https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG',
      in_game:false,
      active:false
    }
    //this.nationalities=this.api.getCountries()
    this.nationalities=["Estados Unidos","México","Costa Rica"]

    //Settinf fault flags to initial false
    this.fault=false
    this.emailalreadytaken=false
  }

}
