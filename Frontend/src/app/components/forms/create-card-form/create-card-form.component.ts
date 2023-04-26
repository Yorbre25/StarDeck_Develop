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
  card !: CardInt;
  types !: string[];
  fault!:boolean;


  //Objetos de input del frontend
  characterName = new FormControl('',[Validators.required]);
  description = new FormControl('',[Validators.required]);
  race = new FormControl('',[Validators.required]);
  energy = new FormControl('',[Validators.required]);
  price = new FormControl('',[Validators.required]);
  type = new FormControl('',[Validators.required]);
  image = new FormControl('',[Validators.required]);

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
    if(this.characterName.invalid||this.description.invalid||this.race.invalid||this.energy.invalid||this.price.invalid||this.type.invalid||this.image.invalid){
      this.fault=true
    }else{
      this.card.name = this.characterName.value
      this.card.description = this.description.value
      this.card.race = this.race.value
      this.card.energy = this.energy.value
      this.card.price = this.price.value
      this.card.type = this.type.value

      this.api.addCard(this.card)//acá llama a la API

    this.router.navigate(['/lobby']);

  }}

  ngOnInit(){
    this.card = 
    { name: "Nombre del Personaje",
      image:"https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
      energy:"100",
      price:"1000",
      type:"MR",
      race:"Cat",
      description: "Nyan Cat, or Pop-Tart Cat, refers to a cartoon cat with a Pop-Tart body and a rainbow behind it, flying through space, set to the tune of a Japanese pop song." };
    //this.nationalities=this.api.getCountries()
    this.types=["MR","SR","SSR","UR"]

    this.fault=false
  }


  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        // Do something with the image data
      };
    }
  }


}
