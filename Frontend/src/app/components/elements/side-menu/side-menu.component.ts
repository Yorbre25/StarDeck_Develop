import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
import { MatDialog } from '@angular/material/dialog';
import {FormBuilder} from '@angular/forms';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ViewChild } from '@angular/core';
import { AccountInt } from '../../interfaces/account.interface';
import { LoginService } from '../../services/login.service';

/**
 * @description
 * This component displays a horizontal navigation bar with the following options and corresponding interactions:
 * button to show user information: opens a block of content that shows user information like:
 *                                  avatar image, username, user level, user points, user rank 
 * logo image: no interaction available 
 * create card button: routes the user to the create card view
 * view cards button: routes the user to the view cards view
 * lobby button: routes the user to the lobby view
 * exit button: routes the user to the initial view 
*/

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})


export class SideMenuComponent implements OnInit{

  public playerinfo?:AccountInt;
  playerInfo = {
    username: "allixe101",
    image: 'https://preview.redd.it/aq38c844r6771.gif?width=640&crop=smart&format=png8&s=8ed07a3fa6871188e6e8d66553aaf90b587f1953',
    rank: 'Gold',
    level: 10,
    points: 5000
  };
  
  options = this._formBuilder.group({
    bottom: 0,
    fixed: false,
    top: 0,
  });
  constructor(private router: Router, private _formBuilder: FormBuilder, private dialog: MatDialog,private logins:LoginService) {}

  showInfo = false;

  ngOnInit(){
    /** 
    this.api.getPlayerInfo(this.logins.getcorreo()).subscribe(data => {
      console.log(data)
      this.playerinfo = data 
    });
    
    console.log("ACAAAAA")
    console.log(this.playerinfo)

    if(this.playerinfo?.avatar!=undefined && this.playerinfo.ranking!=null && this.playerinfo.nickname!=null){
      this.playerInfo.image=this.playerinfo?.avatar
      this.playerInfo.level=this.playerinfo.lvl
      this.playerInfo.points=this.playerinfo.coins
      this.playerInfo.rank=this.playerinfo.ranking
      this.playerInfo.username=this.playerinfo.nickname

    }
    */
  }

  

  

  findGame(){
    const uuid = uuidv4();
    console.log(uuid);
    this.router.navigate(['/partida', uuid]);

  }

  @ViewChild('sidenav')
  sidenav: MatSidenavModule = new MatSidenavModule;
  isExpanded = true;
  showSubmenu: boolean = false;
  isShowing = false;
  showSubSubMenu: boolean = false;

  mouseenter() {
    if (!this.isExpanded) {
      this.isShowing = true;
    }
  }

  mouseleave() {
    if (!this.isExpanded) {
      this.isShowing = false;
    }
  }

  toggleInfo() {
    this.showInfo = !this.showInfo;
  }

  //openDialog(): void {
   // const dialogRef = this.dialog.open(InitialCardChooserComponent);

   // dialogRef.afterClosed().subscribe(result => {
  //    // Handle any actions after the dialog is closed here
 //   });
  
  

//}
}