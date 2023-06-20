import { Injectable } from "@angular/core";
import { DeckInterface } from "../interfaces/deck.interface";
import { CardInt } from "../interfaces/card.interface";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";

@Injectable({
    providedIn:'root'
})

export class deckService{
    url:string="https://localhost:7023/"
    constructor(private http:HttpClient){}

    handleError(error: HttpErrorResponse) {
        console.log(error)
        if(error.error=="Deck name already exist"){
            return throwError(()=>new Error('Deck name already exist.'));
        }else{
            return throwError(()=>new Error('Something bad happened on deck service please try again later.'));
        }
    }

    searchDeckID(deckname:string|null,decks:DeckInterface[]){
        if(deckname!=null){
            for (var deck of decks){
                if(deck.name==deckname){
                    return deck.id
                }
                continue
            }
        }
        console.log("Something went wrong with type")
        return -1
    }

    addDeck(playerID:string|null,deckName:string|null,cards:string[]):Observable<any>{
        let dir =this.url + "Deck/AddDeck"
        console.log(dir)
        let deckforAPI={
            name:deckName,
            playerId:playerID,
            cardIds:cards
        }
        return this.http.post<any>(dir,deckforAPI)
    }

    getAllDecks(playerId:string|null):Observable<DeckInterface[]>{
        let dir = this.url + "Deck/GetDecksFromPlayer/"+playerId
        console.log(dir)
        return this.http.get<DeckInterface[]>(dir)
    }

    getDeckCards(deckId:string|null):Observable<CardInt[]>{
        let dir = this.url + "Deck/GetCardsFromDeck/"+deckId
        console.log(dir)
        return this.http.get<CardInt[]>(dir)
    }

    setDeck(deckId:string|null){
        if(deckId!=null){
            localStorage.setItem("DeckID",deckId)
        }else{
            console.log("Unable to save deck: Null statement")
        }
    }

    getDeck(){
        return localStorage.getItem("DeckID")
    }


}
