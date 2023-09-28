import { Injectable } from '@angular/core';
import {BASE_URL} from "./util";
import {HttpClient} from "@angular/common/http";
import {firstValueFrom} from "rxjs";

const URL = `http://${BASE_URL}/lobby/`;

@Injectable({
  providedIn: 'root'
})
export class LobbyService {

  private lobbyId: string | null;

  constructor(private readonly http: HttpClient) {
    this.lobbyId = null;
  }

  public async requestLobbyStatus(lobbyId: string): Promise<boolean>{
    const response = this.http.get<LoginResult>(URL + lobbyId);
    console.log(response);
    const result = await firstValueFrom(response);
    console.log(result.success);
    if (result.success){
      this.lobbyId = lobbyId;
      console.log(lobbyId);
      return true;
      
    }
    console.log("false");
    return false;

  }
}

interface LoginResult {
  success: boolean;
}
