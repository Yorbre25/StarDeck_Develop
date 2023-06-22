/**
 * @description This component acts as an interface for a card. 
*/

export interface CardInt{
    id:string|null;
    name:string|null;
    image:string|null;
    description:string|null;
    energy:number|undefined;
    cost:number|undefined;
    type:string|null;
    race:string|null;
    
}