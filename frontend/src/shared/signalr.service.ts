/*import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: signalR.HubConnection | null = null;

  constructor() {
  }

  startConnection(lobbyId: number) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5268/hub/${lobbyId}`) // URL anpassen
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
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
}*/

import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  private messageSubject = new Subject<{ user: string; message: string }>();
  public messages$ = this.messageSubject.asObservable();
  private lobbyId: string = '';

  startConnection(lobbyId: string) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5000/chat?lobbyId=${lobbyId}`)
      .build();

    this.lobbyId = lobbyId;

    this.hubConnection.start().catch(err => console.error('Verbindung fehlgeschlagen', err));

    this.hubConnection.on('ReceiveMessage', (user, message) => {
      this.messageSubject.next({ user, message });
    });
  }

  sendMessage(user: string, message: string) {
    this.hubConnection.invoke('SendMessage', user, message, this.lobbyId)
      .catch(err => console.error('Senden fehlgeschlagen', err));
  }
}
