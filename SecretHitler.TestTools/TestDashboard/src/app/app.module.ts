import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {HttpClientModule} from '@angular/common/http';

import {AppComponent} from './app.component';
import {CreateGameComponent} from './create-game/create-game.component';
import {GameService} from './game.service';
import { GameListComponent } from './game-list/game-list.component';
import { GameDetailsComponent } from './game-details/game-details.component';
import { FormsModule } from '@angular/forms';
import { GameStatePipe } from './game-state-pipe.pipe';
import { PlayerComponent } from './player/player.component';
import { RoleTypePipe } from './role-type.pipe';
 
@NgModule({
    declarations: [
        AppComponent,
        CreateGameComponent,
        GameListComponent,
        GameDetailsComponent,
        GameStatePipe,
        PlayerComponent,
        RoleTypePipe
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule
    ],
    providers: [GameService, GameStatePipe],
    bootstrap: [AppComponent]
})
export class AppModule {}
