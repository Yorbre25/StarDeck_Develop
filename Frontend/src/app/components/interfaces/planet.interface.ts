/**
 * @description This component acts as an interface for a planet. 
*/

export interface Planet{
    id:string|null;
    name:string|null;
    image:string|null;
    description:string|null;
    activated_planet:boolean|null;
    type:string|null;
}