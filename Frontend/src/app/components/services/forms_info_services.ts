import { Injectable } from "@angular/core";
import { CountryInterface } from "../interfaces/countryinterface";
import { RaceInterface } from "../interfaces/race.interface";
import { TypeInterface } from "../interfaces/type.interface";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";

@Injectable({
    providedIn:'root'
})

export class FormsService{
    url:string="https://localhost:7023/"
    constructor(private http:HttpClient){}

    handleError(error: HttpErrorResponse) {
        return throwError(()=>new Error('Something bad happened on forms service please try again later.'));
    }

    searchcountryID(countryname:string|null,countries:CountryInterface[]){
        if(countryname!=null){
            for (var country of countries){
                if(country.countryName==countryname){
                    return country.id
                }
                continue
            }
        }
        console.log("Something went wrong with type")
        return -1
    }

    searchtypeID(typename:string|null,types:TypeInterface[]){
        if(typename!=null){
            for (var type of types){
                if(type.typeName==typename){
                    return type.id
                }
                continue
            }
        }
        console.log("Something went wrong with type")
        return -1
    }

    searchraceID(racename:string|null,races:RaceInterface[]){
        if(racename!=null){
            for (var race of races){
                if(race.name==racename){
                    return race.id
                }
                continue
            }
        }
        console.log("Something went wrong with race")
        return -1
    }

    getCountries():Observable<CountryInterface[]>{
        let dir = this.url + "Country/GetAllCountries"
        return this.http.get<CountryInterface[]>(dir)
    }

    getRaces():Observable<RaceInterface[]>{
        let dir = this.url + "Race/GetAllRaces"
        return this.http.get<RaceInterface[]>(dir)
    }

    getTypes():Observable<TypeInterface[]>{
        let dir = this.url + "Type/GetAllCardTypes"
        return this.http.get<TypeInterface[]>(dir)
    }

    getPlanetTypes():Observable<TypeInterface[]>{
        let dir = this.url + "Type/GetAllPlanetTypes"
        return this.http.get<TypeInterface[]>(dir)
    }

}

