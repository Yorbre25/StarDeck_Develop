import { HttpHeaders } from "@angular/common/http";
/**
 * @description This component acts as an interface for a response. 
*/

export interface ResponseI{
    error:string;
    headers:HttpHeaders;
    message:string;
    name:string;
    ok:boolean;
    status:number;
    statusText:string;
    url:string;
}