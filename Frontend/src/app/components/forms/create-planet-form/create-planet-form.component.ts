import { Component } from '@angular/core';
import { PlanetInterface } from '../../interfaces/planet.interface';
import { FormsService } from '../../services/forms_info_services';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { planetService } from '../../services/Planet.service';
import { TypeInterface } from '../../interfaces/type.interface';


@Component({
  selector: 'app-create-planet-form',
  templateUrl: './create-planet-form.component.html',
  styleUrls: ['./create-planet-form.component.scss']
})
export class CreatePlanetFormComponent {

  planet !: PlanetInterface;
  types !: TypeInterface[];

  //Variables de control de ingresod e datos
  fault!: boolean;
  duplicatePlanetNameFault!: boolean;


  //Objetos de input del frontend
  planetName = new FormControl('', [Validators.required]);
  description = new FormControl('', [Validators.required, Validators.maxLength(1000)]);
  type = new FormControl('', [Validators.required]);
  image = new FormControl('', [Validators.required]);

  constructor(private router: Router, private _formBuilder: FormBuilder, private planetService: planetService, private formService:FormsService) { }

  //La idea es que este módul genera el mensaje de error
  getErrMessage(component: FormControl) {
    if (this.description.value != null && this.type.value != null) {
      if (component.hasError('required')) {//El usuario no escribió nada
        return "Este campo es obligatorio."
      } else if (component.hasError('maxlength')) {
        return "La descripción excede los 1000 caracteres posibles"
      } else if (this.duplicatePlanetNameFault) {
        return "Ya existe un planeta con este nombre"
      } else {
        return ""
      }
    } else {
      return "Este campo es obligatorio."
    }
  }

  goToLobby() {
    if (this.description.value != null && this.type.value != null) {
      if (this.planetName.invalid || this.description.invalid || this.type.invalid) {
        this.fault = true
      } else {
        this.fault=false
        this.planet.name = this.planetName.value
        this.planet.description = this.description.value
        this.planet.type = this.type.value
        
           this.planetService.addPlanet(this.planet,this.types).subscribe(//acá llama a la API
          (response) => {
            console.log(response);
            this.router.navigate(['/planets']);
          }, (error) => {
            console.log(error)
            this.duplicatePlanetNameFault = true;
          });
      }
    }
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = () => {
        console.log(reader.result);
        const imageData: string | null = reader.result ? reader.result.toString() : null;
        this.planet.name = this.planetName.value
        this.planet.description = this.description.value
        this.planet.type = this.type.value
        this.planet.image = imageData;
      };
    }
  }

  
  ngOnInit() {
    this.planet =
    {
      id: '',
      name: "Nombre del Planeta",
      image: "https://upload.wikimedia.org/wikipedia/en/e/ed/Nyan_cat_250px_frame.PNG",
      type: "Basico",
      description: "Nyan Cat, or Pop-Tart Cat, refers to a cartoon cat with a Pop-Tart body and a rainbow behind it, flying through space, set to the tune of a Japanese pop song.",
      show: true
    };
    
    this.types = [{
      id:"",
      typeName:""
    }]
    

    this.formService.getPlanetTypes().subscribe((data)=>{
      this.types=this.types.concat(data)
    })
    
    this.fault = false
    this.duplicatePlanetNameFault = false
  }



}
