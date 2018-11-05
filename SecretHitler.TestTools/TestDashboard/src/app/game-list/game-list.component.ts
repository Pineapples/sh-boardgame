import { Component, OnInit } from '@angular/core';
import {GameService} from '../game.service';
import {Game} from '../models/game';

@Component({
  selector: 'game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.scss']
})
export class GameListComponent implements OnInit {
  public games: Game[];

  constructor(private gameService: GameService) { }

  ngOnInit() {
  }

  public refresh() {
    this.gameService.getGames().subscribe((games) => {
      this.games = games;
    });
  }

}
