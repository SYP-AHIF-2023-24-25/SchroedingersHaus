import { Injectable } from '@angular/core';
import {BASE_URL} from "./util";
import {HttpClient} from "@angular/common/http";
import {firstValueFrom} from "rxjs";

const URL = `http://${BASE_URL}/user`;

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userName: string | null;
  private lobbyId: string | null;

  constructor(private readonly http: HttpClient) {
    this.userName = null;
    this.lobbyId = null;
  }

  public async attempUserLogin(userName: string, lobbyId: string): Promise<boolean>{
    const response = this.http.post<LoginResult>(URL, {userName: userName, lobbyId: lobbyId});
    const result = await firstValueFrom(response);
    console.log(result.success);
    if (result.success){
      this.userName = userName;
      this.lobbyId = lobbyId;
      return true;
    }
    return false;
  }

  public get user(): string | null {
    return this.userName;
  }

  public get lobby(): string | null {
    return this.lobbyId;
  }

}

interface LoginResult {
  success: boolean;
}
