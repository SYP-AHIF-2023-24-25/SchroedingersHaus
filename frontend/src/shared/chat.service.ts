/*import {Injectable} from '@angular/core';
import {Observable, Subject} from "rxjs";
import {BASE_URL, SocketClosed, WebSocket} from "./util";

const ENDPOINT_URL = `ws://${BASE_URL}/chat`;

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private socket: WebSocket<string> | null;

  constructor() {
    this.socket = null;
  }

  public sendMessage(message: string): void {
    if (this.socket === null || this.socket.closed){
      throw new SocketClosed();
    }
    this.socket.sendMessage(message);
  }

  public connect(user: string, lobbyId:string): Observable<string> {
    if (this.socket === null) {
      this.socket = new WebSocket<string>(`${ENDPOINT_URL}/${lobbyId}/${user}`);
      console.log("Hey I am here");
      this.socket.errorMessages.subscribe(eMsg => console.log(`WebSocket error: ${eMsg}`));
      this.socket.connect();
    }
    return this.socket.messages;

    if (this.socket === null) {
      this.socket = new WebSocket<string>(`${ENDPOINT_URL}/${lobbyId}/${user}`);
      console.log(`Connecting to WebSocket at: ${ENDPOINT_URL}/${lobbyId}/${user}`);

      this.socket.errorMessages.subscribe(eMsg => console.log(`WebSocket error: ${eMsg}`));
      this.socket.messages.subscribe(msg => {
        console.log(`Message received from WebSocket: ${msg}`);
      });

      this.socket.connect();
      console.log("WebSocket connection established");
    }
    return this.socket.messages;
  }

  public close(): void {
    this.socket?.close();
  }
}*/

/*import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection: signalR.HubConnection | undefined;
  public messages: Subject<{ user: string; message: string }> = new Subject();

  constructor() {}

  public connect(userName: string, lobbyId: string): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5268/chathub/${lobbyId}`)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        // Optional: Hier könntest du eine Nachricht senden, dass der Benutzer dem Chat beigetreten ist
        this.sendMessage(userName, `${userName} joined the chat!`);
      })
      .catch(err => console.error('Error while starting connection: ' + err));

    // Listen für eingehende Nachrichten
    this.hubConnection.on('ReceiveMessage', (user, message) => {
      this.messages.next({ user, message });
    });
  }

  public sendMessage(user: string, message: string): void {
    if (this.hubConnection) {
      this.hubConnection.invoke('SendMessage', user, message)
        .catch(err => console.error('Error while sending message: ' + err));
    }
  }

  public close(): void {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }
}*/


//Last worked version
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private hubConnection: signalR.HubConnection | null = null;

  constructor() {}

  // Start connection with lobbyId
  startConnection(lobbyId: string) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5268/chatHub/${lobbyId}`) // Die lobbyId in die URL integrieren
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR-Verbindung gestartet für Lobby: ' + lobbyId))
      .catch(err => console.log('Fehler beim Start der Verbindung: ' + err));
  }

  sendMessage(user: string, message: string) {
    if (this.hubConnection) {
      this.hubConnection.invoke('SendMessage', user, message)
        .catch(err => console.error('Fehler beim Senden der Nachricht: ', err));
    }
  }

  addReceiveMessageListener(callback: (user: string, message: string) => void) {
    if (this.hubConnection) {
      this.hubConnection.on('ReceiveMessage', callback);
    }
  }
}

/*import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { Observable, Subject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private hubConnection: signalR.HubConnection | null = null;
  private messagesSubject = new Subject<string>();

  public messages$: Observable<string> = this.messagesSubject.asObservable();

  public connect(user: string, lobbyId: string): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:4200/chathub?lobbyId=${lobbyId}&user=${user}`)
      .build();

    this.hubConnection.start()
      .then(() => {
        console.log('SignalR connected.');
        this.hubConnection?.on("ReceiveMessage", (message: string) => {
          this.messagesSubject.next(message);
        });
      })
      .catch(err => console.log("Error establishing connection: " + err));
  }

  public sendMessage(message: string): void {
    if (this.hubConnection) {
      this.hubConnection.invoke("SendMessage", message)
        .catch(err => console.error("Error sending message: " + err));
    }
  }

  public close(): void {
    this.hubConnection?.stop().catch(err => console.log("Error closing connection: " + err));
  }
}*/
