import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-back-button',
  templateUrl: './back-button.component.html',
  styleUrls: ['./back-button.component.scss']
})
export class BackButtonComponent {
  constructor(private router: Router, private location: Location) {}

  goBack() {
    if (this.location.path() === '') {
      this.router.navigate(['/home']);
    } else {
      this.location.back();
    }
  }
}
