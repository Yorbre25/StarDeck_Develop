import { Injectable } from "@angular/core";
import { catchError, Observable } from "rxjs";
import { AccountInt } from "../interfaces/account.interface";
import { CountryInterface } from "../interfaces/countryinterface";
import { FormsService } from "./forms_info_services";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";

@Injectable({
    providedIn:'root'
})

export class LoginService{
    public id!:string
    public correo!:string

    constructor(private http:HttpClient,private formService:FormsService){}

    url:string="https://localhost:7023/"

    
    handleError(error: HttpErrorResponse) {
        console.log(error)
        if(error.error=="Player username already exist"){
            return throwError(()=>new Error('Player username already exist.'));    
        }else if(error.error=="Player email already exist"){
            return throwError(()=>new Error('Player email already exist.'));
        }else{
            return throwError(()=>new Error('Something bad happened at login service. Please try again later.'));
        }
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
            countryId:this.formService.searchcountryID(player.country,countries)
        }
        console.log(playerforAPI)
        return this.http.post(dir,playerforAPI).pipe(catchError(this.handleError))
    }
    
    getAllPlayers():Observable<AccountInt[]>{
        let dir = this.url + "Player/GetAllPlayers"
        console.log(dir)
        return this.http.get<AccountInt[]>(dir)
    }


    getPlayerID(playermail:string|null,allPlayers:AccountInt[]){ 
        for (var player of allPlayers){
            if(player.email==playermail){
                return player.id
            }
        }
        console.log("Something went wrong with account")
        return "ERROR"
    }

    Login(mail:string|null,password:string|null):Observable<any>{
        let dir=this.url+"Login/"+mail+"/"+password
        console.log(dir)
        return this.http.get(dir).pipe(catchError(this.handleError))
    }


    getloggedin(){
        return localStorage.getItem("logged?")
    }

    getloggedinadmin(){
        return localStorage.getItem("adminlogged?")
    }

    getcorreo(){
        return localStorage.getItem("mail")
    }

    getid(){
        return localStorage.getItem("id")
    }

    setid(id:string){
        localStorage.setItem("id",id)
    }

    setcorreo(Correo:string){
        localStorage.setItem("mail",Correo)
    }

    setloggedinpl(Logged:string){
        localStorage.setItem("logged?",Logged)
    }

    setloggedadmin(Logged:string){
        localStorage.setItem("adminlogged?",Logged) 
    }

}