import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})

export class LoginService{
    public id!:string
    public correo!:string
    
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