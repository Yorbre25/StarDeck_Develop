import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
import { InitialCardChooserComponent } from '../../pop-ups/initial-card-chooser/initial-card-chooser.component';
import {FormBuilder} from '@angular/forms';
import { DialogConfig } from '@angular/cdk/dialog';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { CardService } from '../../services/Card.service';
import { AccountInt } from '../../interfaces/account.interface';
import { LoginService } from '../../services/login.service';
import { gameService } from '../../services/Game.service';

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
export class LobbyComponent implements OnInit{
  showPopup=true;
  allplayers!:AccountInt[];
  Ammount ?:number;
  InitAmountCards:number=18;

  options = this._formBuilder.group({
    bottom: 0,
    fixed: false,
    top: 0,
  });
  

  constructor(private router: Router, private _formBuilder: FormBuilder, private dialog: MatDialog, private cardService:CardService,private gameService:gameService ,private logs:LoginService) {}

  openDialog(){ // hacer que verifique que el usuario tenga 18 cartas para ver si ensena el popup o no 
    
    if (this.showPopup){
      const dialogConfig = new MatDialogConfig();

      dialogConfig.disableClose = true;
      dialogConfig.autoFocus = true;
      dialogConfig.maxHeight = 500;
      dialogConfig.maxWidth = 1100;
  
      this.dialog.open(InitialCardChooserComponent, dialogConfig);
    };
    
  }

  findGame(){
    const uuid = uuidv4();
    console.log(uuid);
    this.router.navigate(['/match/choose_deck', uuid]);
  }
    
  ngOnInit(): void {
    this.cardService.getAmCards(this.logs.getid()).subscribe((data)=>{
      console.log("Card Amount")
      console.log(data)
      let gameID:string|null=""
      if(data<this.InitAmountCards){
        this.showPopup=true
        this.openDialog() 
      }
      this.gameService.GetAllGames().subscribe((data)=>{
        for (var Game of data){
          if (this.logs.getid()!=null && (this.logs.getid()==Game.player1Id || this.logs.getid()==Game.player2Id)){
              gameID=Game.id
              this.gameService.SetParameters(Game.player1Id,Game.player2Id,Game.id)   
              console.log(this.gameService.GameInfo)         
          }
        }
        if(gameID!=undefined && gameID!=""){
          console.log("Player already in match")
          this.gameService.setgameID(gameID)
          this.gameService.SetUpHands().subscribe((data)=>{})
          const uuid = uuidv4();
          console.log(uuid);
          this.router.navigate(['/match', uuid]);
        }
      })
    })
  }
}
