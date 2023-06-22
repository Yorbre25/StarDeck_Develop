import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUserComponent } from './components/views/create/create-user/create-user.component';
import { GameComponent } from './components/views/game-setup/game/game.component';
import { LobbyComponent } from './components/views/lobby/lobby.component';
import { LoginComponent } from './components/views/login/login.component';
import { CardMenuComponent } from './components/views/menu/card-menu/card-menu.component';
import { CreateCardComponent } from './components/views/create/create-card/create-card.component';
import { DeckMenuComponent } from './components/views/menu/deck-menu/deck-menu.component';
import { CreateDeckComponent } from './components/views/create/create-deck/create-deck.component';
import { SingleDeckComponent } from './components/views/single-deck/single-deck.component';
import { CreatePlanetComponent } from './components/views/create/create-planet/create-planet.component';
import { PlanetMenuComponent } from './components/views/menu/planet-menu/planet-menu.component';
import { LoadingGameComponent } from './components/views/game-setup/loading-game/loading-game.component';
import { DeckSelectComponent } from './components/views/game-setup/deck-select/deck-select.component';

const routes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'sign_up', component: CreateUserComponent},
  { path: 'home', component: LobbyComponent},
  { path: 'match/:id', component: GameComponent},
  { path: 'match/choose_deck/:id', component: DeckSelectComponent},
  { path: 'create_card', component: CreateCardComponent},
  { path: 'cards', component: CardMenuComponent},
  { path: 'decks', component: DeckMenuComponent},
  { path: 'deck', component: SingleDeckComponent},
  { path: 'create_deck', component: CreateDeckComponent},
  { path: 'create_planet', component: CreatePlanetComponent},
  { path: 'planets', component: PlanetMenuComponent},
  { path: 'searching', component: LoadingGameComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
