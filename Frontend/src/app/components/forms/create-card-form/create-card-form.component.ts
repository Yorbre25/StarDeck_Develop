import { Component, OnInit } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
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
  races !: string[];
  fault!: boolean;


  //Objetos de input del frontend
  characterName = new FormControl('', [Validators.required]);
  description = new FormControl('', [Validators.required]);
  race = new FormControl('', [Validators.required]);
  energy = new FormControl('', [Validators.required]);
  price = new FormControl('', [Validators.required]);
  type = new FormControl('', [Validators.required]);
  image = new FormControl('', [Validators.required]);

  constructor(private router: Router, private _formBuilder: FormBuilder, private api: ApiService) { }



  //La idea es que este módul genera el mensaje de error
  getErrMessage(component: FormControl) {
    if (component.hasError('required')) {//El usuario no escribió nada
      return "Este campo es obligatorio."
    } else if (component.value.length > 30) {//La contraseña no cuenta con la longitud requerida
      return "Las contraseña debe tener un tamaño de 8 caracteres"
    } else if (component.invalid) {//El usuario se pasa de la cantidad de caracteres
      return "El usuario debe tener entre 1 y 30 caracteres"
    }
    else {
      return ""
    }
  }

  /**
   * El nombre de la carta debe tener entre 5 y 30 caracteres y la descripción debe ser de hasta 1000 caracteres.  
  El sistema debe de manera predeterminada marcar la carta en estado activa.
  Los valores de energía es numéricos y puede ir en un rango de -100 a 100.
  El costo en batalla es numéricos y puede ir desde 0 hasta 100. 
  El tipo de carta debe seleccionarse de 5 valores posibles: Ultra-Rara, Muy Rara, Rara, Normal, Básica.
  La raza de la carta debe ser seleccionada de una lista pre-cargada con las razas oficiales.
   */


  goToLobby() {
    if (this.characterName.invalid || this.description.invalid || this.race.invalid || this.energy.invalid || this.price.invalid || this.type.invalid || this.image.invalid) {
      this.fault = true
    } else if (this.description.value?.length! >= 1000 || this.description.value?.length! <= 0) {
      this.fault = true
    } else if (this.energy.value?.length! >= 100 || this.description.value?.length! <= -100) {
      this.fault = true
    } else if (this.price.value?.length! >= 100 || this.description.value?.length! <= 0) {
      this.fault = true
    } else {
      this.card.name = this.characterName.value
      this.card.description = this.description.value
      this.card.race = this.race.value
      this.card.energy = this.energy.value
      this.card.price = this.price.value
      this.card.type = this.type.value
      this.card.image = this.image.value

      this.api.addCard(this.card)//acá llama a la API

      this.router.navigate(['/home']);

    }
  }

  ngOnInit() {
    this.card =
    {
      name: "Nombre del Personaje",
      image: "https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
      energy: "100",
      price: "1000",
      type: "MR",
      race: "Cat",
      description: "Nyan Cat, or Pop-Tart Cat, refers to a cartoon cat with a Pop-Tart body and a rainbow behind it, flying through space, set to the tune of a Japanese pop song."
    };
    //this.nationalities=this.api.getCountries()
    this.types = ["UR", "MR", "R", "N", "B"]
    this.races = ["Humano", "Cyborg", "Alien", "Robot", "Angel", "Demonio", "Pirata", "Elemental", "Dragon", "Asesino", "Mascota"]
    this.fault = false
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
