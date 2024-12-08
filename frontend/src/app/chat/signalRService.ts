/*import { Injectable } from '@angular/core';
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
      .withUrl(`http://localhost:5000/chathub?lobbyId=${lobbyId}`)
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
}*/
