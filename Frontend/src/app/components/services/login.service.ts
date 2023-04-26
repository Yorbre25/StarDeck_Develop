import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})

export class LoginService{
    public id!:string
    public correo!:string

    getcorreo(){
        return this.correo
    }

    getID(){
        return this.id
    }

    setID(ID:string){
        this.id=ID
    }

    setcorreo(Correo:string){
        this.correo=Correo
    }

}