import { Component } from '@angular/core';
import {LobbyService} from "../../shared/lobby.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";


@Component({
  selector: 'app-login-lobby',
  templateUrl: './login-lobby.component.html',
  styleUrls: ['./login-lobby.component.scss']
})
export class LoginLobbyComponent {

  public lobbyId: string | null;
  public inProgress: boolean;

  constructor(private readonly lobbyService: LobbyService,
              private readonly snackBar: MatSnackBar,
              private readonly router: Router) {
    this.lobbyId = null;
    this.inProgress = false;
  }

  public async loginLobby(): Promise<void> {
    this.inProgress = true;
    //Lobby can only be created by the Unity Game
    const lobbyOk = await this.lobbyService.requestLobbyStatus(this.lobbyId!);
    this.inProgress = false;
    if (lobbyOk){
      console.log('lobby existiert');
      const navRes = await this.router.navigateByUrl(`login-user/${this.lobbyId}`);
      if (!navRes){
        console.log('Navigation failed');
      }
      return;
    }
    console.log('lobby existiert nicht');
    this.lobbyId = null;

    //Snack bar should maybe disapear itself
    this.snackBar.open('Lobby does not exist!', 'Close', {
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
  }
}
