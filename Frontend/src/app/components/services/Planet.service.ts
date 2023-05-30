import { Injectable } from "@angular/core";
import { FormsService } from "./forms_info_services";
import { TypeInterface } from "../interfaces/type.interface";
import { PlanetInterface } from "../interfaces/planet.interface";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";


@Injectable({
    providedIn:'root'
})

export class planetService{
    url:string="https://localhost:7023/"
    constructor(private http:HttpClient, private formService:FormsService){}

    handleError(error: HttpErrorResponse) {
        console.log(error)
        if(error.error=="Planet name already exist"){
            return throwError(()=>new Error('Planet name already exist.'));
        }else{
            return throwError(()=>new Error('Something bad happened at planet service. please try again later.'));
        }
    }

    
    
    addPlanet(planet:PlanetInterface,types:TypeInterface[]):Observable<any>{
        let dir = this.url+"Planet/AddPlanet"
        let planetforAPI={
            name:planet.name,
            typeId:this.formService.searchtypeID(planet.type,types),
            description:planet.description,
            image:planet.image
        }
        console.log(planetforAPI)
        return this.http.post<any>(dir,planetforAPI).pipe(catchError(this.handleError))
    }

    




    

    getAllPlanets():Observable<PlanetInterface[]>{
        let dir = this.url+"Planet/GetAllPlanets"
        console.log(dir)
        return this.http.get<PlanetInterface[]>(dir)
    }

    

}