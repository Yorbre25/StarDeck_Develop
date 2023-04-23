import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MatCardModule } from '@angular/material/card';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LobbyComponent } from './components/views/lobby/lobby.component';
import { GameComponent } from './components/views/game/game.component';
import { LoginComponent } from './components/views/login/login.component';
import { CreateUserComponent } from './components/views/create-user/create-user.component';
import { CardComponent } from './components/elements/card/card.component';
import { MultipleCardsComponent } from './components/elements/multiple-cards/multiple-cards.component';
import { LoginFormComponent } from './components/forms/login-form/login-form.component';
import { SignUpFormComponent } from './components/forms/sign-up-form/sign-up-form.component';
import { InitialCardChooserComponent } from './components/pop-ups/initial-card-chooser/initial-card-chooser.component';
import { CardChooserComponent } from './components/action/card-chooser/card-chooser.component';
import { CardMenuComponent } from './components/views/card-menu/card-menu.component';
import { SideMenuComponent } from './components/elements/side-menu/side-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    LobbyComponent,
    GameComponent,
    LoginComponent,
    CreateUserComponent,
    CardComponent,
    MultipleCardsComponent,
    LoginFormComponent,
    SignUpFormComponent,
    InitialCardChooserComponent,
    CardChooserComponent,
    CardMenuComponent,
    SideMenuComponent
  ],
  imports: [
    MatFormFieldModule,
    MatCardModule,
    BrowserModule,
    AppRoutingModule,
    NoopAnimationsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    MatListModule
  ],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { floatLabel: 'always' } }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
