import { Injectable } from "@angular/core";
import { CardInt } from "../interfaces/card.interface";

@Injectable({
    providedIn:'root'
})

export class seleced_Card_S{
    
    
    getcard(){
        return localStorage.getItem("scard")
    }

    setcard(card:CardInt){
        if(card.id!=null){
            localStorage.setItem("scard",card.id)
        }
    }

}