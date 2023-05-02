/**
 * @description This component acts as an interface for user account. 
*/

export interface AccountInt{
    id:string|null;
    email:string|null;
    firstName:string|null;
    lastName:string|null;
    username:string|null;
    pHash:string|null;
    level:number;
    ranking:string|null;
    country:string|null;
    coins:number;
    avatar:string;
}