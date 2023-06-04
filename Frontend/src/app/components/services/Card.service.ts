import { Injectable } from "@angular/core";
import { CardInt } from "../interfaces/card.interface";
import { RaceInterface } from "../interfaces/race.interface";
import { TypeInterface } from "../interfaces/type.interface";
import { FormsService } from "./forms_info_services";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";

@Injectable({
    providedIn:'root'
})

export class CardService{
    url:string="https://localhost:7023/"
    constructor(private http:HttpClient, private formService:FormsService){}

    handleError(error: HttpErrorResponse) {
        console.log(error)
        if(error.error=="Card name already exist"){
            return throwError(()=>new Error('Card name already exist.'));
        }else{
            return throwError(()=>new Error('Something bad happened at Card service. Please try again later.'));
        }

        
    }

    getAmCards(player:string|null):Observable<number>{
        let dir = this.url + "PlayerCard/CardCount/"+player
        return this.http.get<number>(dir)
    }
    
    assignPlayerInitialCards(playerid:string|null):Observable<any>{
        let dir= this.url + "PlayerCard/GenerateCardsForNewPlayer/"+playerid
        console.log(dir)
        console.dir(playerid)
        return this.http.post(dir,{})
    }

    getplayerCards(player:string|null):Observable<CardInt[]>{
        let dir = this.url +"PlayerCard/GetPlayerCards/"+player
        console.log(dir)
        return this.http.get<CardInt[]>(dir)
    }

    playerchoseCard(cardId:string|null,playerId:string|null):Observable<any>{
        let dir = this.url + 'PlayerCard/AssignCardToPlayer/'+playerId+"/"+cardId
        console.log(dir)
        return this.http.post<any>(dir,{})
    }

    getCard(cardId:string):Observable<CardInt>{
        let dir = this.url + "Card/GetCardById/"+cardId
        console.log(dir)
        return this.http.get<CardInt>(dir)
    }

    getchoosingcard():Observable<CardInt[][]>{
        let dir = this.url+'PlayerCard/GetPackagesForNewPlayer'
        return this.http.get<CardInt[][]>(dir)
    }


    addCard(card:CardInt,types:TypeInterface[],races:RaceInterface[]):Observable<any>{
        let dir =this.url + "Card/AddCard"
        console.log("dir: "+ dir)
        console.log(card)
        let cardforAPI={
            name:card.name,
            energy:card.energy,
            cost:card.cost,
            description:card.description,
            image:card.image,
            typeId:this.formService.searchtypeID(card.type,types),
            raceId:this.formService.searchraceID(card.race,races)
        }
        console.log(cardforAPI)
        return this.http.post<any>(dir,cardforAPI).pipe(catchError(this.handleError))
    }


    getAllCards():Observable<CardInt[]>{
        let dir = this.url + "Card/GetAllCards"
        console.log(dir)
        return this.http.get<CardInt[]>(dir)
    }


}