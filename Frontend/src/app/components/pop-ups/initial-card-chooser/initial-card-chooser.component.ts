import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatDialogActions } from '@angular/material/dialog';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
@Component({
  selector: 'app-initial-card-chooser',
  templateUrl: './initial-card-chooser.component.html',
  styleUrls: ['./initial-card-chooser.component.scss']
})
export class InitialCardChooserComponent {



    constructor(private dialogRef: MatDialogRef<InitialCardChooserComponent>) {}

    close() {
      this.dialogRef.close();
  }


}
