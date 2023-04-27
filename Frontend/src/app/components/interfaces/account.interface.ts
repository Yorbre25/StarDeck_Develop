/**
 * @description This component acts as an interface for user account. 
*/

export interface AccountInt{
    id:string|null;
    email:string|null;
    f_name:string|null;
    l_name:string|null;
    nickname:string|null;
    p_hash:string|null;
    lvl:number;
    ranking:string|null;
    in_game:boolean;
    active:boolean;
    country:string|null;
    coins:number;
    avatar:string;
}