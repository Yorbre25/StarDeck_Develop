import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateUserComponent } from './components/views/create-user/create-user.component';
import { GameComponent } from './components/views/game/game.component';
import { LobbyComponent } from './components/views/lobby/lobby.component';
import { LoginComponent } from './components/views/login/login.component';

const routes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'create', component: CreateUserComponent},
  { path: 'lobby', component: LobbyComponent},
  { path: 'partida/:id', component: GameComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
