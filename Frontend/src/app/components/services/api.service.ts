import { Injectable } from "@angular/core";
import { AccountInt } from "../interfaces/account.interface";
import { ResponseI } from "../interfaces/response.interface";
import { CountryInterface } from "../interfaces/countryinterface";
import { CardInt } from "../interfaces/card.interface";
import { RaceInterface } from "../interfaces/race.interface";
import { TypeInterface } from "../interfaces/type.interface";
import { HttpClient,HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";
import { RouterTestingHarness } from "@angular/router/testing";


@Injectable({
    providedIn:'root'
})

export class ApiService{
    url:string="https://localhost:7023/"

    constructor(private http:HttpClient){}

    handleError(error: HttpErrorResponse) {
        return throwError(()=>new Error('Something bad happened; please try again later.'));
    }

    getAmCards(player:string|null):Observable<number>{
        let dir = this.url + "CardAssign/card_count/"+player
        return this.http.get<number>(dir)
    }

    registerAccount(player:AccountInt):Observable<any>{
        let dir = this.url + "api/Player"
        console.log("dir: "+ dir)
        console.log(player)
        return this.http.post(dir,player).pipe(catchError(this.handleError))
    }
    
    getPlayerInfo(player:string|null):Observable<AccountInt>{
        let dir = this.url + "api/Player/"+player
        console.log(dir)
        return this.http.get<AccountInt>(dir)
    }

    addCard(card:CardInt):Observable<any>{
        let dir =this.url + "Card/AddCard"
        console.log("dir: "+ dir)
        console.log(card)
        return this.http.post<ResponseI>(dir,card).pipe(catchError(this.handleError))
    }

    getAllCards():Observable<CardInt[]>{
        let dir = this.url + "Card/GetAllCards"
        console.log(dir)
        return this.http.get<CardInt[]>(dir)
      }

    getplayerCards(player:string|null):Observable<CardInt[]>{
        let dir = this.url +"CardAssign/"+player
        console.log(dir)
        return this.http.get<CardInt[]>(dir)
    }

    getCard(cardId:string):Observable<CardInt>{
        let dir = this.url + "GetCardById/"+cardId
        console.log(dir)
        return this.http.get<CardInt>(dir)
    }

    playerchoseCard(cardId:string|null,playerId:string|null):Observable<ResponseI>{
        let dir = this.url + 'CardAssign/'+playerId
        console.log(dir)
        let card;
        if(cardId!=null)
            this.getCard(cardId).subscribe(data=>{
                card=data
            })
        console.log(card)
        return this.http.post<ResponseI>(dir,card)
    }

    getchoosingcard(player:string|null):Observable<CardInt[][]>{
        let dir = this.url+'CardAssign/GetPackagesForNewPlayer'
        return this.http.get<CardInt[][]>(dir)
    }

    getCountries():Observable<CountryInterface[]>{
        let dir = this.url + "api/Country"
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
    

}