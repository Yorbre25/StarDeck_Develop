import { Injectable } from "@angular/core";
import { CardInt } from "../interfaces/card.interface";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn:'root'
})

export class selected_Card_S{
    private cardList=new BehaviorSubject<string[]>([]);

    cardList$ = this.cardList.asObservable();
    
    
    addCardtoList(cardid:string){
        const oldList=this.cardList.getValue();
        var newarray:string[]=[];
        var addcard:boolean=true;
        for(var index in oldList){
            if(oldList[index]==cardid){
                addcard=false
                continue
            }else{
                newarray.push(oldList[index])
            }
        }
        if(addcard){
            newarray.push(cardid)
        }
        this.cardList.next(newarray);
    }

    getcardList(){
        return this.cardList.getValue()
    }

    initializeCardList(){
        this.cardList.next([])
    }

    getcard(){
        return localStorage.getItem("scard")
    }

    setcard(card:CardInt){
        if(card.id!=null){
            localStorage.setItem("scard",card.id)
            this.addCardtoList(card.id)
        }
    }

}