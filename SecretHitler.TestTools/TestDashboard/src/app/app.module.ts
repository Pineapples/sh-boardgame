import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {HttpClientModule} from '@angular/common/http';

import {AppComponent} from './app.component';
import {CreateGameComponent} from './create-game/create-game.component';
import {GameService} from './game.service';

@NgModule({
    declarations: [
        AppComponent,
        CreateGameComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule
    ],
    providers: [GameService],
    bootstrap: [AppComponent]
})
export class AppModule {}
