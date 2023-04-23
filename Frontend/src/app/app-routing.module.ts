import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUserComponent } from './components/views/create-user/create-user.component';
import { GameComponent } from './components/views/game/game.component';
import { LobbyComponent } from './components/views/lobby/lobby.component';
import { LoginComponent } from './components/views/login/login.component';
import { CardMenuComponent } from './components/views/card-menu/card-menu.component';
import { CreateCardComponent } from './components/views/create-card/create-card.component';


const routes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'sign_up', component: CreateUserComponent},
  { path: 'lobby', component: LobbyComponent},
  { path: 'partida/:id', component: GameComponent},
  { path: 'create_card', component: CreateCardComponent},
  { path: 'cards', component: CardMenuComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
