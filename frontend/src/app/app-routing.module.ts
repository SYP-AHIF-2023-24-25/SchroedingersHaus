import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginUserComponent} from "./login-user/login-user.component";
import {LoginLobbyComponent} from "./login-lobby/login-lobby.component";
import {ChatComponent} from "./chat/chat.component";
import {AuthGuard} from "../shared/auth.guard";
import {TeamComponent} from "./team/team.component";
import {HowToPlayComponent} from "./how-to-play/how-to-play.component";
import {TrailerComponent} from "./trailer/trailer.component";
import {CodeComponent} from "./code/code.component";

const routes: Routes = [
  {path: 'login-lobby', component: LoginLobbyComponent},
  {path: 'team', component: TeamComponent},
  {path: 'howTo', component: HowToPlayComponent},
  {path: 'trailer', component: TrailerComponent},
  {path: 'code', component: CodeComponent},


  {path: 'login-user/:id', component: LoginUserComponent},
  {path: 'chat', component: ChatComponent, canActivate: [AuthGuard]},
  {path: 'diary', component: ChatComponent, canActivate: [AuthGuard]},
  {path: '**', redirectTo: 'login-lobby'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
