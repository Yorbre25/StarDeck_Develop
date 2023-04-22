import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LobbyComponent } from './components/views/lobby/lobby.component';
import { GameComponent } from './components/views/game/game.component';
import { LoginComponent } from './components/views/login/login.component';
import { CreateUserComponent } from './components/views/create-user/create-user.component';
import { CardComponent } from './components/card/card.component';
import { MultipleCardsComponent } from './components/multiple-cards/multiple-cards.component';
import { LoginFormComponent } from './components/forms/login-form/login-form.component';

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
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
