import { Component, NgModule, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {FormBuilder, FormControl} from '@angular/forms';
import { LoginService } from '../../services/login.service';
import { Validators } from '@angular/forms';


/**
 *@description This component acts as a user register form.
*/


@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']

})

export class LoginFormComponent implements OnInit {
  fault!:boolean;
  wrongdata!:boolean;
  validalphanumpassword!:boolean;

  validPattern:string="^[a-zA-Z0-9]{8}$";
  
  mail = new FormControl('',[Validators.required,Validators.email]);
  playerPassword = new FormControl('',[Validators.required,Validators.pattern(this.validPattern)]);


  constructor(private router: Router, private _formBuilder: FormBuilder, private loginService:LoginService) {}
  
  
  goToLobby(){
    if(this.mail.invalid || this.playerPassword.invalid){
      this.fault=true
      this.wrongdata=false
      this.validalphanumpassword=false
    }else if(this.playerPassword.value!=null&&(!(/[a-zA-Z]/.test(this.playerPassword.value))||!(/\d/.test(this.playerPassword.value)))){
      this.fault=true
      this.wrongdata=false
      this.validalphanumpassword=true
    }else{
      this.fault=false    
      this.loginService.Login(this.mail.value,this.playerPassword.value).subscribe(
        (response)=>{
          this.loginService.getAllPlayers().subscribe((data)=>{
            let allPlayers=data
            if(this.playerPassword.value!=null && this.mail.value!=null){
              this.loginService.setcorreo(this.mail.value)
              let ID=this.loginService.getPlayerID(this.mail.value,allPlayers)
              if(ID!=null){
                this.loginService.setid(ID)
                this.loginService.setloggedinpl("true")
                this.router.navigate(['/home']);    
              }
            }
          })
        },(error)=>{
          this.fault=false
          this.wrongdata=true
        }
      );
    }
  }

  getErrMessage(component:FormControl){
    if(component.hasError('required')){//El usuario no escribió nada
      return "Este campo es obligatorio"
    }else if(component.hasError('email')){
      return "Ingrese un mail valido"
    }else if(component.hasError('pattern')){
      return "Las contraseña debe tener un tamaño de 8 caracteres y debe ser alfanumérica"
    }else if(!(/[a-zA-Z]/.test(component.value))){
      return "la contraseña debe de contar con al menos un valor alfabetico"
    }else if(!(/\d/.test(component.value))){
      return "la contraseña debe de contar con al menos un valor numerico"
    }else if(this.wrongdata){
      return "El correo o la contraseña proporcionados no son correctos"
    }else{
      return ""
    }
  }


  ngOnInit(): void {
    this.wrongdata=false
    this.fault=false
  }
}
