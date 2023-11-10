import {Component, OnDestroy, OnInit} from '@angular/core';
import {ChatService} from "../../shared/chat.service";
import {UserService} from "../../shared/user.service";
import {ActivatedRoute, Params, Router} from "@angular/router";
import diaryData from './desktop.json';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {

  public userName: string | null;
  public lobbyId: string | null;
  public messages: MessageDisplay[] | null;
  public message: string | null;
  public users: string | null;

  private page: number = 0;
  diary: any = diaryData[this.page];

  constructor(private readonly chatService: ChatService,
              private readonly userService: UserService,
              private router: Router,
              private activatedRoute: ActivatedRoute) {
    this.userName = null;
    this.lobbyId = null;
    this.messages = null;
    this.message = null;
    this.users = null;
  }

  nextPage() {
    if(this.page <= 17){
      this.page++;
      this.diary = diaryData[this.page];
    }else{
      this.diary = diaryData[this.page];
    }
  }

  previousPage() {
    if(this.page >= 0){
      this.page--;
      this.diary = diaryData[this.page];
    }else{
      this.diary = diaryData[this.page];
    }
  }

  chat() {
    this.router.navigate(['/chat'])
  }


  public ngOnInit(): void {
    this.userName = this.userService.user;
    this.lobbyId = this.userService.lobby;
    console.log(this.lobbyId, this.userName);
    console.log(this.users);
    this.startChat();
  }

  public ngOnDestroy(): void {
    this.chatService.close();
  }

  public sendMessage(): void {
    this.sendMsg(this.message!);
    this.message = null;
  }

  public get displayMessages(): MessageDisplay[] {
    return this.messages?.reverse() ?? [];
  }

  private sendMsg(msg: string): void {
    this.chatService.sendMessage(msg);
  }

  private startChat(): void {
    this.messages = [];
    this.chatService.connect(this.userName!,this.lobbyId!)
      .subscribe(msg => {
        this.messages?.push(new MessageDisplay(msg));
      });
    this.sendMsg(`${this.userName} joined the Party!`);
  }

  /*private getAllUsers (): void {
    this.chatService.GetAllUsersFromLobby(this.lobbyId!)
      .subscribe(users => {
        this.users = users;
      });
  }*/
  /* Diary sollte ein Fenster öffnen oder in chat componente drinnen sein
  diary() {
    this.router.navigate(['/diary']);
  }*/

}

class MessageDisplay {

  public readonly dateTime: Date;

  constructor(public readonly message: string) {
    this.dateTime = new Date();
  }
}
