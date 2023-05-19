import { Injectable } from "@angular/core";
import { MatchInterface } from "../interfaces/Matchmaking.interface";
import { GameSetupInit } from "../interfaces/gamesetupinit";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";


@Injectable({
    providedIn:'root'
})

export class gameService{
    url:string="https://localhost:7023/"
    constructor(private http:HttpClient){}
    
    SearchGame(playerID:string,deckID:string):Observable<MatchInterface>{
        let dir = this.url+"Match_Player/"+playerID+"/"+deckID
        return this.http.get(dir)
    }

    CancelSearch():Observable<any>{
        let dir = this.url+"Match_Player/cancel-long-runnuing"
        return this.http.get(dir) 
    }

    SetParameters(Connection:GameSetupInit):Observable<MatchInterface>{
        let dir = this.url+"SetupParameters"
        return this.http.post(dir,Connection)
    }

    GetGamePlanets():Observable<any>{
        let dir = this.url+"GetGamePlanets/"+this.getgameID()
        return this.http.post(dir,{})
    }

    SetUpHands():Observable<any>{
        let dir = this.url+"SetupHands/"+this.getgameID()
        return this.http.post(dir,{})
    }

    GetHandCards(playerID:string):Observable<any>{
        let dir = this.url+"GetHandCards/"+this.getgameID()+"/"+playerID
        return this.http.get(dir)
    }


    setgameID(gameID:string){
        localStorage.setItem("GameID",gameID)
    }

    getgameID(){
        localStorage.getItem("GameID")
    }
}