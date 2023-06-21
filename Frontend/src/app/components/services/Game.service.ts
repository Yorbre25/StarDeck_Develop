import { Injectable } from "@angular/core";
import { MatchInterface } from "../interfaces/Matchmaking.interface";
import { SetUpInterface } from "../interfaces/setup.interface";
import { UsersInfoGame } from "../interfaces/GameUsersInfo.interface";
import { GameSetupInit } from "../interfaces/gamesetupinit";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import {map, catchError, Observable, throwError } from "rxjs";
import { convertToParamMap } from "@angular/router";


@Injectable({
    providedIn:'root'
})

export class gameService{
    url:string="https://localhost:7023/"
    
    public GameInfo:SetUpInterface={
    id:"G-4qxupc239iqp",
    totalTurns:10,
    timePerTurn:20,
    currentTurn:0,
    player1Id:"U-9bgk7mcknhjq",
    player2Id:"U-3896g2e7muv4",
    userNamePlayer1:"Sample 1",
    userNamePlayer2:"Sample 2",
    deckNamePlayer1:"NO",
    deckNamePlayer2: "NO",
    initialCardPoints:10,
    cardsPerPlanet:5
    };

    
    constructor(private http:HttpClient){}
   
    handleError(error: HttpErrorResponse) {
        console.log(error)
        return throwError(()=>new Error('An error occured please try again later'));    
        
    }

    SearchGame(playerID:string|null,deckID:string|null):Observable<any>{
        let dir = this.url+"Match_Player/"+playerID+"/"+deckID
        return this.http.get(dir)
    }

    GetAllGames():Observable<any>{
        let dir = this.url+"api/Game/GetGames"
        console.log(dir)
        return this.http.get(dir)
    }
    
    GetGamePlayers():Observable<any>{
        let dir = this.url + "api/Game/GetPlayers"
        console.log(dir)
        return this.http.get(dir)
    }

    SetParameters(player1Id:string,player2Id:string,GameID:string){
        this.GameInfo.player1Id=player1Id
        this.GameInfo.player2Id=player2Id
        this.GameInfo.id=GameID
    }

    CancelSearch():Observable<any>{
        let dir = this.url+"Match_Player/cancel-long-runnuing"
        return this.http.get(dir) 
    }

    

    GetGamePlanets():Observable<any>{
        let dir = this.url+"GetGamePlanets/"+this.getgameID()
        return this.http.post(dir,{})
    }

    SetUpHands():Observable<any>{
        let dir = this.url+"SetupHands/"+this.getgameID()
        return this.http.post(dir,{})
    }

    GetHandCards(playerID:string|null):Observable<any>{
        let dir = this.url+"GetHandCards/"+this.getgameID()+"/"+playerID
        return this.http.get(dir)
    }


    setgameID(gameID:string){
        localStorage.setItem("GameID",gameID)
    }

    getgameID(){
        return localStorage.getItem("GameID")
    }

    getplayerinfo(playerID:string|null){
        let UsersGameinfo:UsersInfoGame={
            OpTag:"NOT SUPPOSED TO HAPPEN",
            Ptag:"NOT SUPPOSED TO HAPPEN",
        };
        if(playerID==this.GameInfo.player1Id){
            UsersGameinfo.OpTag=this.GameInfo.player2Id,
            UsersGameinfo.Ptag=this.GameInfo.player1Id
        }else if(playerID==this.GameInfo.player2Id){
            UsersGameinfo.OpTag=this.GameInfo.player1Id,
            UsersGameinfo.Ptag=this.GameInfo.player2Id
        }
        return UsersGameinfo
    }
    getGameValues(){
        let gameValues={
            totalTurns:this.GameInfo.totalTurns,
            timePerTurn:this.GameInfo.timePerTurn,
            currentTurn:this.GameInfo.currentTurn
        }
        return gameValues
    }

    drawCard(playerID:string|null):Observable<any>{
        let dir =this.url +"DrawCard/"+this.getgameID()+"/"+playerID
        console.log(dir)
        return this.http.post(dir,{}).pipe(catchError(this.handleError))
    }
}