import { Injectable } from "@angular/core";
import { AccountInt } from "../interfaces/account.interface";
import { ResponseI } from "../interfaces/response.interface";
import { HttpClient,HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn:'root'
})

export class ApiService{
    url:string="https://localhost:####/"

    constructor(private http:HttpClient){}
    
    //registerAccount(player:AccountInt):Observable<ResponseI>{
    registerAccount(player:AccountInt){
        let dir = this.url + "Direccion"
        console.log("dir: "+ dir)
        console.log(player)
        //return this.http.post<ResponseI>(dir,player)
        return player
    }

}