import { Injectable } from "@angular/core";
import { CardInt } from "../interfaces/card.interface";

@Injectable({
    providedIn:'root'
})

export class seleced_Card_S{
    private cardList!:string[];


    addCardList(card:CardInt){
        console.log(card)
        if(card.id!=null){
            if (card.id in this.cardList){
                let index=this.cardList.indexOf(card.id)
                this.cardList.splice(index,1)
            }else{
                if(this.cardList.length!=5)
                    this.cardList.push(card.id)
                else{
                    console.log("Max amount of cards in deck")
                }   
            }
        }
        console.log(this.cardList)
    }

    resetCardList(){
        this.cardList=[]
    }

    getCardList(){
        return this.cardList
    }

    getcard(){
        return localStorage.getItem("scard")
    }

    setcard(card:CardInt){
        if(card.id!=null){
            localStorage.setItem("scard",card.id)
            this.addCardList(card)
        }
    }

}