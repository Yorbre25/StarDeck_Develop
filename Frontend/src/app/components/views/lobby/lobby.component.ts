import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
import { InitialCardChooserComponent } from '../../pop-ups/initial-card-chooser/initial-card-chooser.component';
import {FormBuilder} from '@angular/forms';
import { DialogConfig } from '@angular/cdk/dialog';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { LoginService } from '../../services/login.service';

/**
 * @description 
 * This component acts as a view for the game lobby. 
 * 
 * @typedef {class} LobbyComponent
 * 
 * @property {CardInt[]} cards - cards to be displayed. 
 * @property {boolean} showPopup - used to determine if pop up should be shown.
 * @property {CardInt[]} cards - cards to be displayed. 
 * @property {Function} openDialog - The function to call when the dialog should open.

*/
@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.scss']
})
export class LobbyComponent{
  showPopup=true;
  Ammount ?:number;

  options = this._formBuilder.group({
    bottom: 0,
    fixed: false,
    top: 0,
  });

  constructor(private router: Router, private _formBuilder: FormBuilder, private dialog: MatDialog, private api:ApiService, private logs:LoginService) {
    
    /*this.api.getAmCards(this.logs.getid()).subscribe(data=>{
      console.log(data)
      this.Ammount=data
    })
    console.log(this.Ammount)
  if(this.Ammount!=undefined){
    this.showPopup=this.Ammount<18
    this.openDialog()  
  }else{
    console.log("Something wrong")
  }*/

    this.showPopup=true
    this.openDialog() 
  }

  openDialog(){ // hacer que verifique que el usuario tenga 18 cartas para ver si ensena el popup o no 
    
    if (this.showPopup){
      const dialogConfig = new MatDialogConfig();

      dialogConfig.disableClose = true;
      dialogConfig.autoFocus = true;
      dialogConfig.maxHeight = 500;
      dialogConfig.maxWidth = 1100;
  
      this.dialog.open(InitialCardChooserComponent, dialogConfig);
      //this.showPopup = false;
    };
    
  }
  findGame(){
    const uuid = uuidv4();
    console.log(uuid);
    this.router.navigate(['/partida', uuid]);
    

  }
}
