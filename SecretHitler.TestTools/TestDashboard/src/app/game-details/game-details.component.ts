import { Component, OnInit } from '@angular/core';
import {GameService} from '../game.service';
import {Game} from '../models/game';

@Component({
  selector: 'game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss']
})
export class GameDetailsComponent implements OnInit {
  public selectedId: number;
  public game: Game;
  public addPlayerModel = { "name": null };
  constructor(private gameService: GameService) { }

  ngOnInit() {
  }

  getGame(gameId){
    this.gameService.getGame(gameId).subscribe((game) => {
      console.log(game);
      this.game = game;
    });
  }

  startGame(gameId) {
    this.gameService.startGame(gameId).subscribe((game) => {
      this.game = game;
    })
  }

  addPlayer() {
    this.gameService.joinGame(this.addPlayerModel.name, this.game.joinKey).subscribe((player) => {
      this.getGame(player.gameId)
      console.log(player);
    });
    this.addPlayerModel.name = null;
  }
}
