import { Injectable } from "@angular/core";
import { MatchInterface } from "../interfaces/Matchmaking.interface";
import { SetUpInterface } from "../interfaces/setup.interface";
import { UsersInfoGame } from "../interfaces/GameUsersInfo.interface";
import { GameSetupInit } from "../interfaces/gamesetupinit";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";


@Injectable({
    providedIn:'root'
})

export class gameService{
    url:string="https://localhost:7023/"
    
    private GameInfo:SetUpInterface={
    id:"G-s2zty98u4088",
    gameTableId:"GT-buj96mf0z1rx",
    totalTurns:10,
    timePerTurn:20,
    currentTurn:0,
    player1Id:"U-2f4qp1hke60c",
    player2Id:"U-7lgnnhc5yvkv",
    userNamePlayer1:"Blondie",
    userNamePlayer2:"Sample",
    deckNamePlayer1:"Migentelatino",
    deckNamePlayer2: "SapleDeck"
    };

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
            PDeckN:"NOT SUPPOSED TO HAPPEN"
        };
        if(playerID==this.GameInfo.player1Id){
            UsersGameinfo.OpTag=this.GameInfo.userNamePlayer2,
            UsersGameinfo.Ptag=this.GameInfo.userNamePlayer1,
            UsersGameinfo.PDeckN=this.GameInfo.deckNamePlayer1
        }else if(playerID==this.GameInfo.player2Id){
            UsersGameinfo.OpTag=this.GameInfo.userNamePlayer1,
            UsersGameinfo.Ptag=this.GameInfo.userNamePlayer2,
            UsersGameinfo.PDeckN=this.GameInfo.deckNamePlayer2
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

    drawcard(playerID:string):Observable<any>{
        let dir =this.url +"DrawCard/"+this.getgameID()+"/"+playerID
        console.log(this.drawcard)
        return this.http.post(dir,{})
    }
}