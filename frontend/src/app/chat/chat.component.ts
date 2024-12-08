/*import {Component, ElementRef, OnDestroy, OnInit, ViewChild,  ChangeDetectorRef } from '@angular/core';
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
  @ViewChild('liveImage') liveImage!: ElementRef<HTMLImageElement>;
  private intervalId: any;
  public imageUrl = 'http://http://192.168.0.13/:8080/lobby/dJaqO3/screenshot';


  private page: number = 0;
  diary: any = diaryData[this.page];

  constructor(private readonly chatService: ChatService,
              private readonly userService: UserService,
              private router: Router,
              private activatedRoute: ActivatedRoute,
              private cdr: ChangeDetectorRef) {
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
    /*this.intervalId = setInterval(() => {
      this.reloadImage();
    }, 2000);//
  }

  public ngOnDestroy(): void {
    this.chatService.close();
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  public sendMessage(): void {
    this.sendMsg(this.message!);
    this.message = null;
  }

  public get displayMessages(): MessageDisplay[] {
    console.log("Display messages:", this.messages);
    //return this.messages?.reverse() ?? [];
    //return [...(this.messages || [])].reverse();
    return this.messages ? [...this.messages].reverse() : [];
  }

  private sendMsg(msg: string): void {
    this.chatService.sendMessage(msg);
  }

  private startChat(): void {
    this.messages = [];
    this.chatService.connect(this.userName!,this.lobbyId!)
      .subscribe(msg => {
        const newMessage = new MessageDisplay(msg);
      if (!this.messages!.some(m => m.message === newMessage.message)) {
        this.messages!.push(newMessage);
        this.cdr.detectChanges(); // UI-Update erzwingen
      }
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

  /*reloadImage(): void {
    const imgElement = this.liveImage.nativeElement;
    imgElement.src = this.imageUrl + '?' + new Date().getTime();
  }//

}

class MessageDisplay {

  public readonly dateTime: Date;

  constructor(public readonly message: string) {
    this.dateTime = new Date();
  }
}*/

/*import { Component, OnDestroy, OnInit } from '@angular/core';
import { ChatService } from '../../shared/chat.service';
import { UserService } from '../../shared/user.service';
import diaryData from './desktop.json';
import { NgModule } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {
  public userName: string | null;
  public lobbyId: string | null;
  public messages: { user: string, message: string }[] = [];
  public message: string = '';

  private page: number = 0;
  diary: any = diaryData[this.page];

  constructor(private chatService: ChatService, private userService: UserService) {
    this.userName = null;
    this.lobbyId = null;
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

  ngOnInit(): void {
    this.userName = this.userService.user;
    this.lobbyId = this.userService.lobby;

    // Hier die Verbindung zum Chat herstellen
    this.chatService.connect(this.userName!, this.lobbyId!);
    this.sendJoinMessage();
    this.chatService.messages.subscribe(msg => {
      this.messages.push(msg);
    });
  }

  private sendJoinMessage(): void {
    const joinMessage = { user: this.userName!, message: `${this.userName} joined the chat!` };
    this.chatService.sendMessage(joinMessage.user, joinMessage.message);
  }

  public sendMessage(message: string): void {
    if (message) {
      const messageObject = {
          user: this.userName!,
          message: this.message
      };
      this.chatService.sendMessage(messageObject.user, messageObject.message);
      this.message = ""; // Clear the message input after sending
    }
  }

  public get displayMessages(): { user: string, message: string, dateTime: Date }[] {
    return this.messages.map(msg => ({
      user: msg.user,
      message: msg.message,
      dateTime: new Date() // Hier kannst du das Datum der Nachricht verwenden, falls vorhanden
    })).reverse();
  }

  ngOnDestroy(): void {
    // Hier könntest du eventuell Ressourcen freigeben oder Verbindungen schließen
  }
}*/

/*import { Component, OnInit, OnDestroy } from '@angular/core';
import { ChatService } from '../../shared/chat.service';
import { UserService } from '../../shared/user.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {
  public userName: string | null = null;
  public lobbyId: string | null = null;
  public messages: { user: string, message: string }[] = [];
  public message: string | null = null;

  constructor(private chatService: ChatService, private userService: UserService) {}

  ngOnInit(): void {
    this.userName = this.userService.user;
    this.lobbyId = this.userService.lobby;

    if (this.lobbyId) {
      // Start SignalR connection with the lobbyId
      this.chatService.startConnection(this.lobbyId);
    }

    this.chatService.addReceiveMessageListener((user, message) => {
      this.messages.push({ user, message });
    });
  }

  sendMessage(): void {
    if (this.message && this.userName) {
      this.chatService.sendMessage(this.userName, this.message);
      this.message = ''; // Nachrichteneingabe zurücksetzen
    }
  }

  ngOnDestroy(): void {
    // Optional: Hier könntest du die Verbindung schließen, wenn nötig
  }
}*/

/*import { SignalRService } from '../../shared/signalr.service';
import { Component } from '@angular/core';
import diaryData from './desktop.json';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html'
})
export class ChatComponent {
  public user: string = ''; // Benutzername
  public message: string = ''; // Nachricht
  public lobbyId: string | null = null;
  public messages: Array<{ user: string; message: string }> = [];


  private page: number = 0;
  diary: any = diaryData[this.page];

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

  constructor(private signalRService: SignalRService) {
    this.signalRService.addReceiveMessageListener((user, message) => {
      this.messages.push({ user, message });
    });
    this.lobbyId = null;
  }

  sendMessage() {
    this.signalRService.sendMessage(this.user, this.message);
    this.message = ''; // Eingabefeld zurücksetzen
  }
}*/



//Last worked version
import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../../shared/signalr.service';
import { UserService } from '../../shared/user.service';
import { ActivatedRoute } from '@angular/router';
import diaryData from './desktop.json';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
})
export class ChatComponent implements OnInit {
  user: string = ''; // Benutzername
  message: string = ''; // Nachricht
  messages: Array<{ user: string; message: string }> = []; // Array für Nachrichten
  public lobbyId: string = ""; //| null = null;

  constructor(private signalRService: SignalRService, private route: ActivatedRoute, private userService: UserService) {
    //this.lobbyId = null;
  }

  private page: number = 0;
  diary: any = diaryData[this.page];

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

  ngOnInit() {
    this.user = this.userService.user ?? '';
    this.lobbyId = this.userService.lobby ?? '';

    if (this.lobbyId) {
      this.signalRService.startConnection(this.lobbyId);
    }

    this.signalRService.messages$.subscribe(msg => {
      this.messages.push(msg);
    });
  }

  sendMessage() {
    if (this.user && this.message && this.lobbyId) {
      this.signalRService.sendMessage(this.user, this.message);
      this.messages.push({ user: this.user, message: this.message });
      this.message = '';
    }
  }
}

/*import { Component, OnDestroy, OnInit } from '@angular/core';
import { ChatService } from "../../shared/chat.service";
import { UserService } from "../../shared/user.service";
import { ActivatedRoute, Params, Router } from "@angular/router";
import diaryData from './desktop.json';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {

  public userName: string | null;
  public lobbyId: string | null;
  public messages: string[] = [];
  public message: string | null;

  private messageSubscription: Subscription | null = null;

  constructor(private readonly chatService: ChatService,
              private readonly userService: UserService,
              private router: Router,
              private activatedRoute: ActivatedRoute) {
    this.userName = null;
    this.lobbyId = null;
    this.message = null;
  }

  private page: number = 0;
  diary: any = diaryData[this.page];

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
  public ngOnInit(): void {
    this.userName = this.userService.user;
    this.lobbyId = this.userService.lobby;
    if (this.userName && this.lobbyId) {
      this.chatService.connect(this.userName, this.lobbyId);
      this.messageSubscription = this.chatService.messages$.subscribe(message => {
        this.messages.push(message);
      });
      this.sendMsg(`${this.userName} joined the Party!`);
    }
  }

  public ngOnDestroy(): void {
    this.chatService.close();
    this.messageSubscription?.unsubscribe();
  }

  public sendMessage(): void {
    this.sendMsg(this.message!);
    this.message = null;
  }

  private sendMsg(msg: string): void {
    this.chatService.sendMessage(msg);
  }
}*/
