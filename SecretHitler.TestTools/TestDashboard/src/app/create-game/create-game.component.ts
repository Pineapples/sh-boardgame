import { Component, OnInit } from '@angular/core';
import {GameService} from '../game.service';
import {Game} from '../models/game';
import {Observable} from 'rxjs';

@Component({
  selector: 'create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent {
  public game: Observable<Game>;

  constructor(private gameService: GameService) { }

  createGame() {
    this.game = this.gameService.createGame();
  }
}
