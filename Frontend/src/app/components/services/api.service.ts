import { Injectable } from "@angular/core";
import { AccountInt } from "../interfaces/account.interface";
import { ResponseI } from "../interfaces/response.interface";
import { CardInt } from "../interfaces/card.interface";
import { HttpClient,HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";
import { RouterTestingHarness } from "@angular/router/testing";

@Injectable({
    providedIn:'root'
})

export class ApiService{
    url:string="https://localhost:7023/api/"

    constructor(private http:HttpClient){}

    handleError(error: HttpErrorResponse) {
        return throwError(()=>new Error('Something bad happened; please try again later.'));
    }

    getAmCards(player:string|null):Observable<number>{
        let dir = this.url + "Player_Card/card_count/"+player
        return this.http.get<number>(dir)
    }

    registerAccount(player:AccountInt):Observable<any>{
        let dir = this.url + "Player"
        console.log("dir: "+ dir)
        console.log(player)
        return this.http.post(dir,player).pipe(catchError(this.handleError))
    }
    
    getPlayerInfo(player:string|null):Observable<AccountInt>{
        let dir = this.url + "Player/"+player
        console.log(dir)
        return this.http.get<AccountInt>(dir)
    }

    addCard(card:CardInt):Observable<any>{
        let dir =this.url + "Card"
        console.log("dir: "+ dir)
        console.log(card)
        return this.http.post<ResponseI>(dir,card).pipe(catchError(this.handleError))
    }

    getAllCards():Observable<CardInt[]>{
        let dir = this.url + "Card"
        console.log(dir)
        return this.http.get<CardInt[]>(dir)
      }

    getplayerCards(player:string|null):Observable<CardInt[]>{
        let dir = this.url +"Player_Card/"+player
        console.log(dir)
        return this.http.get<CardInt[]>(dir)
    }
    playerchoseCard(card:string|null,player:string|null):Observable<ResponseI>{
        let dir = this.url + 'PLayer_Card/'+player+'/'+card
        let pack={
            player_id:player,
            card_id:card
        }
        console.log(pack)
        
        return this.http.post<ResponseI>(dir,pack)
    }

    getchoosingcard(player:string|null):Observable<CardInt[]>{
        let dir = this.url+'PLayer_Car/'+player+'/'+'3'
        return this.http.get<CardInt[]>(dir)
    }

    getCountries():Observable<string[]>{
        let dir = this.url + "Dirección 4"
        return this.http.get<string[]>(dir)
    }

    

}