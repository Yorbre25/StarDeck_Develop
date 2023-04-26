import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
import { InitialCardChooserComponent } from '../../pop-ups/initial-card-chooser/initial-card-chooser.component';
import {FormBuilder} from '@angular/forms';
import { DialogConfig } from '@angular/cdk/dialog';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.scss']
})
export class LobbyComponent{
  options = this._formBuilder.group({
    bottom: 0,
    fixed: false,
    top: 0,
  });
  constructor(private router: Router, private _formBuilder: FormBuilder, private dialog: MatDialog) {
    this.openDialog()
  }

  openDialog(){
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.maxHeight = 500;
    dialogConfig.maxWidth = 1100;

    this.dialog.open(InitialCardChooserComponent, dialogConfig);

  }
  findGame(){
    const uuid = uuidv4();
    console.log(uuid);
    this.router.navigate(['/partida', uuid]);
    

  }

  //openDialog(): void {
   // const dialogRef = this.dialog.open(InitialCardChooserComponent);

   // dialogRef.afterClosed().subscribe(result => {
  //    // Handle any actions after the dialog is closed here
 //   });
  
  

//}
}
