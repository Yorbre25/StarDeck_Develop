import { Injectable } from "@angular/core";
import { AccountInt } from "../interfaces/account.interface";
import { ResponseI } from "../interfaces/response.interface";
import { CardInt } from "../interfaces/card.interface";
import { HttpClient,HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn:'root'
})

export class ApiService{
    url:string="https://localhost:####/"

    constructor(private http:HttpClient){}
    
    registerAccount(player:AccountInt):Observable<ResponseI>{
        let dir = this.url + ""
        console.log("dir: "+ dir)
        console.log(player)
        return this.http.post<ResponseI>(dir,player)
    }
    addCard(card:CardInt):Observable<ResponseI>{
        let dir =this.url + "Dirección 2"
        console.log("dir: "+ dir)
        console.log(card)
        return this.http.post<ResponseI>(dir,card)
    }

    getAllCards():Observable<CardInt[]>{
        let dir = this.url + "Dirección 3"
        return this.http.get<CardInt[]>(dir)
      }

    getCountries():Observable<string[]>{
        let dir = this.url + "Dirección 4"
        return this.http.get<string[]>(dir)
    }

}