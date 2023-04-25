import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { v4 as uuidv4 } from 'uuid';
import { MatDialog } from '@angular/material/dialog';
import { InitialCardChooserComponent } from '../../pop-ups/initial-card-chooser/initial-card-chooser.component';
import {FormBuilder} from '@angular/forms';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ViewChild } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent {
  options = this._formBuilder.group({
    bottom: 0,
    fixed: false,
    top: 0,
  });
  constructor(private router: Router, private _formBuilder: FormBuilder) {}
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

  //openDialog(): void {
   // const dialogRef = this.dialog.open(InitialCardChooserComponent);

   // dialogRef.afterClosed().subscribe(result => {
  //    // Handle any actions after the dialog is closed here
 //   });
  
  

//}
}