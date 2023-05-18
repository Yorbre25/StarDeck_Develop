import { Injectable } from "@angular/core";
import { catchError, Observable } from "rxjs";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";

@Injectable({
    providedIn:'root'
})

export class LoginService{
    public id!:string
    public correo!:string

    constructor(private http:HttpClient){}

    url:string="https://localhost:7023/"

    
    handleError(error: HttpErrorResponse) {
        console.log(error)
        return throwError(()=>new Error('Somthing wrong'));
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