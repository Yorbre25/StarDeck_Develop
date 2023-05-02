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
        console.log(error)
        if(error.error=="Player username already exist"){
            return throwError(()=>new Error('Player username already exist.'));    
        }else if(error.error=="Player email already exist"){
            return throwError(()=>new Error('Player email already exist.'));
        }else{
            return throwError(()=>new Error('Something bad happened; please try again later.'));
        }
    }

    getAmCards(player:string|null):Observable<number>{
        let dir = this.url + "CardAssign/card_count/"+player
        return this.http.get<number>(dir)
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

    getPlayerID(playermail:string|null){
        var allplayers:AccountInt[]
        allplayers=[]
        this.getAllPlayers().subscribe(//acá llama a la API
          (data) => {
            allplayers=data
        });

        for (var player of allplayers){
            if(player.email==playermail){
                return player.id
            }
        }
        console.log("Something went wrong with account")
        return "ERROR"
        }
    
    registerAccount(player:AccountInt, countries:CountryInterface[]):Observable<any>{
        let dir = this.url + "Player/AddPlayer"
        console.log("dir: "+ dir)
        console.log(player)
        let playerforAPI={
            email:player.email,
            firstName:player.firstName,
            lastName:player.lastName,
            username:player.username,
            password:player.pHash,
            avatar:player.avatar,
            countryId:this.searchcountryID(player.country,countries)
        }
        console.log(playerforAPI)
        return this.http.post(dir,playerforAPI).pipe(catchError(this.handleError))
    }
    
    getAllPlayers():Observable<AccountInt[]>{
        let dir = this.url + "Player/GetAllPlayers"
        console.log(dir)
        return this.http.get<AccountInt[]>(dir)
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
            typeId:this.searchtypeID(card.type,types),
            raceId:this.searchraceID(card.race,races)
        }
        console.log(cardforAPI)
        return this.http.post<ResponseI>(dir,cardforAPI).pipe(catchError(this.handleError))
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
    

}