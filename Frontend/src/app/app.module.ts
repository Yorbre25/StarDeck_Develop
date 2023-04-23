import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LobbyComponent } from './components/views/lobby/lobby.component';
import { GameComponent } from './components/views/game/game.component';
import { LoginComponent } from './components/views/login/login.component';
import { CreateUserComponent } from './components/views/create-user/create-user.component';
import { CardComponent } from './components/elements/card/card.component';
import { MultipleCardsComponent } from './components/elements/multiple-cards/multiple-cards.component';
import { LoginFormComponent } from './components/forms/login-form/login-form.component';

import { MatCardModule } from '@angular/material/card';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormField, MatFormFieldModule } from '@angular/material/form-field';

@NgModule({
  declarations: [
    AppComponent,
    LobbyComponent,
    GameComponent,
    LoginComponent,
    CreateUserComponent,
    CardComponent,
    MultipleCardsComponent,
    LoginFormComponent
  ],
  imports: [
    MatFormFieldModule,
    MatCardModule,
    BrowserModule,
    AppRoutingModule,
    NoopAnimationsModule,
  ],
  providers: [
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {floatLabel: 'always'}}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
