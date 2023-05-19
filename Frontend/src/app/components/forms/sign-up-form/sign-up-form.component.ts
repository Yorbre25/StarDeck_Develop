import { Component, OnInit } from '@angular/core';
import { AccountInt } from '../../interfaces/account.interface';
import { Router } from '@angular/router'; 
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import { CardService } from '../../services/Card.service';
import { LoginService } from '../../services/login.service';
import { FormsService } from '../../services/forms_info_services';
import { CountryInterface } from '../../interfaces/countryinterface';

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
  nationalities !: CountryInterface[];
  fault!:boolean;
  emailalreadytaken!:boolean;
  useralreadytaken!:boolean;
  validalphanumpassword!:boolean;
  validPattern:string="^[a-zA-Z0-9]{8}$";
    



  //Objetos de input del frontend
  mail = new FormControl('',[Validators.required,Validators.email]);
  playerName = new FormControl('',[Validators.required]);
  playerLastName = new FormControl('',[Validators.required]);
  playerNationality = new FormControl('',[Validators.required]);
  playerAlias = new FormControl('',[Validators.required,Validators.maxLength(30)]);
  playerPassword = new FormControl('',[Validators.required,Validators.pattern(this.validPattern)]);
  confirmPassword = new FormControl('',[Validators.required]);

  
  
  constructor(private router: Router, private _formBuilder: FormBuilder, private loginService:LoginService, private cardService:CardService, private formService:FormsService) {}

  //La idea es que este módulo genera el mensaje de error
  getErrMessage(component:FormControl){
    if(component.hasError('required')){//El usuario no escribió nada
      return "Este campo es obligatorio"
    }else if(component.hasError('email')){
      return "Ingrese un mail valido"
    }else if(component.hasError('maxlength')){
      return "El usuario debe tener entre 1 y 30 caracteres"
    }else if(component.hasError('pattern')){
      return "Las contraseña debe tener un tamaño de 8 caracteres y debe ser alfanumérica"
    }else if(this.emailalreadytaken){
      return ("El correo proporcionado ya se encuentra registrado")
    }else if(this.useralreadytaken){
      return ("Ya existe un jugador con ese alias porfavor elija otro")
    }else if(!(/[a-zA-Z]/.test(component.value))){
      return "la contraseña debe de contar con al menos un valor alfabetico"
    }else if(!(/\d/.test(component.value))){
      return "la contraseña debe de contar con al menos un valor numerico"
    }else if(this.playerPassword.value!=this.confirmPassword.value){
      return "Las contraseñas deben coincidir"
    }else{
      return ""
    }
  }


  //Valida la información y guardará en caso que todo esté correcto
  goToLobby(){
    if(this.mail.invalid||this.playerName.invalid||this.playerLastName.invalid||this.playerNationality.invalid||this.playerAlias.invalid||this.playerPassword.invalid){
      this.fault=true
      this.emailalreadytaken=false
      this.useralreadytaken=false
      this.validalphanumpassword=false
    }else if(this.playerPassword.value!=this.confirmPassword.value){
      this.fault=true
      this.emailalreadytaken=false
      this.useralreadytaken=false
      this.validalphanumpassword=false
    }else if(this.playerPassword.value!=null&&(!(/[a-zA-Z]/.test(this.playerPassword.value))||!(/\d/.test(this.playerPassword.value)))){
      this.fault=true
      this.useralreadytaken=false
      this.emailalreadytaken=false
      this.validalphanumpassword=true
    }else{
      this.fault=false
      this.validalphanumpassword=false
      this.user.email = this.mail.value
      this.user.firstName = this.playerName.value
      this.user.lastName = this.playerLastName.value
      this.user.country = this.playerNationality.value
      this.user.username = this.playerAlias.value
      this.user.pHash=this.playerPassword.value
      this.loginService.registerAccount(this.user,this.nationalities).subscribe( //acá llama a la API
        (response) => {
          if(this.user.email!=null){  
            this.loginService.setcorreo(this.user.email) //Guarda el correo del usuario que está actualmente loggeado
          }
          this.loginService.getAllPlayers().subscribe((data)=>{
            this.user.id=this.loginService.getPlayerID(this.user.email,data)  
            this.cardService.assignPlayerInitialCards(this.user.id).subscribe((response)=>{
              console.log(response)
              if(this.user.id!=null){
                this.loginService.setid(this.user.id)
              }
              this.router.navigate(['/home']);
              })
            })
             
        },(error)=>{
          if(error.message=="Player username already exist."){
            this.useralreadytaken=true
            this.emailalreadytaken=false
          }else if(error.message=="Player email already exist."){
            this.emailalreadytaken=true
            this.useralreadytaken=false
          }else{
            console.log("Something wrong with the request")
          }
        }
      );
    } 
  }


  //Inicializa las variables
  ngOnInit(){
    this.user = {
      id:'',
      email:'',
      firstName:'',
      lastName:'',
      country:'',
      username:'',
      pHash:'',
      ranking:'',
      level:0,
      coins:20,//
      avatar:'https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG',
    }
    this.nationalities=[{
      id:"",
      countryName:""
    }]
    this.formService.getCountries().subscribe((data)=>{
      this.nationalities=this.nationalities.concat(data)
    })

    //Settinf fault flags to initial false
    this.fault=false
    this.emailalreadytaken=false
    this.useralreadytaken=false
    this.validalphanumpassword=false
  }

}
