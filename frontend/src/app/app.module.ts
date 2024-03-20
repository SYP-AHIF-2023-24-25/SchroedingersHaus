import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatCardModule} from "@angular/material/card";
import { ChatComponent } from './chat/chat.component';
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {FormsModule} from "@angular/forms";
import { LoginUserComponent } from './login-user/login-user.component';
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {HttpClientModule} from "@angular/common/http";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import { LoginLobbyComponent } from './login-lobby/login-lobby.component';
import { DiaryComponent } from './diary/diary.component';
import { DesktopComponent } from './desktop/desktop.component';
import { StartComponent } from './start/start.component';
import { TeamComponent } from './team/team.component';
import { HowToPlayComponent } from './how-to-play/how-to-play.component';
import { TrailerComponent } from './trailer/trailer.component';
import { CodeComponent } from './code/code.component';

@NgModule({
  declarations: [
    AppComponent,
    ChatComponent,
    LoginLobbyComponent,
    LoginUserComponent,
    StartComponent,
    DiaryComponent,
    DesktopComponent,
    TeamComponent,
    HowToPlayComponent,
    TrailerComponent,
    CodeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    MatProgressSpinnerModule,
    HttpClientModule,
    MatSnackBarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
