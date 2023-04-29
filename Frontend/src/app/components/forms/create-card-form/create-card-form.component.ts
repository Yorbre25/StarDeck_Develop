import { Component, OnInit } from '@angular/core';
import { CardInt } from '../../interfaces/card.interface';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { RouterTestingHarness } from '@angular/router/testing';

/**
 * @description
 * This component acts as a user register form for card creation. The required fields are:
 * character name, character description, race, energy, price, type and image.
 * 
 * @typedef {class} CreateCardFormComponent
 * 
 * @property {CardInt} card- card to be created. 
 * @property {string[]} types - array of available types. 
 * @property {string[]} races - array of available races.
 * @property {string} fault - Indicates if there is an error in the user inputs.  
 * 
 * @property {string} characterName - card character name. 
 * @property {string} description - card character description. 
 * @property {string} race - card character race. 
 * @property {string} energy- card character energy. 
 * @property {string} price - card character price. 
 * @property {string} type - card character type.
 * @property {string} image - card character image. 
 * 
 * @property {Function} onSubmit - The function to call when the form is created displays a card with placeholder information.
 * @property {Function} goToLobby - The function to call when the form is submitted.
 * @property {Function} onFileSelected - The function to call when the image is submitted.
*/

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

  //Variables de control de ingresod e datos
  fault!: boolean;
  pricerangefault!:boolean;
  energyrangefault!:boolean;
  duplicatecardnamefault!:boolean;


  //Objetos de input del frontend
  characterName = new FormControl('', [Validators.required]);
  description = new FormControl('', [Validators.required,Validators.maxLength(1000)]);
  race = new FormControl('', [Validators.required]);
  energy = new FormControl('', [Validators.required]);
  price = new FormControl('', [Validators.required]);
  type = new FormControl('', [Validators.required]);
  image = new FormControl('', [Validators.required]);
  
  constructor(private router: Router, private _formBuilder: FormBuilder, private api: ApiService) { }



  //La idea es que este módul genera el mensaje de error
  getErrMessage(component: FormControl) {
    if(this.energy.value!=null && this.price.value!=null){
      if (component.hasError('required')) {//El usuario no escribió nada
        return "Este campo es obligatorio."
      } else if (component.hasError('maxlength')) {
        return "La descripción excede los 1000 caracteres posibles"
      } else if(this.duplicatecardnamefault){
        return "Ya existe una carta con este nombre"
      }else if (+this.energy.value > 100 || +this.energy.value < -100) {
        return "La energía debe tener un valor entre -100 y 100"
      } else if (+this.price.value > 100 || +this.price.value <= 0) {
        return "El costo debe tener un valor entre 0 y 100"
      } else {
        return ""
      }
    }else{
      return "Este campo es obligatorio."
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
    if(this.energy.value!=null&&this.price.value!=null){
      if (this.characterName.invalid||this.description.invalid||this.race.invalid||this.energy.invalid||this.price.invalid||this.type.invalid) {
        this.fault = true
      } else if (+this.energy.value >= 100 || +this.energy.value <= -100) {
        this.energyrangefault = true
      } else if (+this.price.value >= 100 || +this.price.value <= 0) {
        this.pricerangefault = true
      } else {
        this.pricerangefault=false
        this.energyrangefault=false
        this.card.name = this.characterName.value
        this.card.description = this.description.value
        this.card.race = this.race.value
        this.card.energy = +this.energy.value
        this.card.cost = +this.price.value
        this.card.type = this.type.value
      

        this.api.addCard(this.card).subscribe(//acá llama a la API
          (response) => {
            console.log(response);
            this.router.navigate(['/home']);
          },(error)=>{
            console.log(error)
            this.duplicatecardnamefault=true;
          });

    }}
  }

  ngOnInit() {
    this.card =
    {
      id: '',
      name: "Nombre del Personaje",
      image: "https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
      energy: 100,
      cost: 1000,
      type: "MR",
      race: "Cat",
      description: "Nyan Cat, or Pop-Tart Cat, refers to a cartoon cat with a Pop-Tart body and a rainbow behind it, flying through space, set to the tune of a Japanese pop song.",
      activated_card: true
    };
    
    this.types = ["Ultra Rara", "Muy Rara", "Rara", "Normal", "Basica"]
    this.races = ["Humano", "Cyborg", "Alien", "Robot", "Angel", "Demonio", "Pirata", "Elemental", "Dragon", "Asesino", "Mascota"]
    this.fault = false
    this.energyrangefault = false
    this.pricerangefault = false
    this.duplicatecardnamefault = false
  }


  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = () => {
        console.log(reader.result);
        const imageData: string | null = reader.result ? reader.result.toString() : null;
        this.card.name = this.characterName.value
          this.card.description = this.description.value
          this.card.race = this.race.value
          
          this.card.type = this.type.value
          
        this.card.image = imageData;
      };
    }
  }


}
