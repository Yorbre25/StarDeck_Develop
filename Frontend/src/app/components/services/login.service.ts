import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})

export class LoginService{
    public id!:string
    public correo!:string
    

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

}