import { Component, OnInit, Input } from '@angular/core';
import {Player} from '../models/player';
import {Game} from '../models/game';
import {GameService} from '../game.service';

@Component({
  selector: '[player]',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.scss']
})
export class PlayerComponent implements OnInit {
  @Input()
  public player: Player;

  @Input() 
  public game: Game;

  public choiceNumber: number;

  constructor(private gameService: GameService) { }

  ngOnInit() {
  }

  public vote(inFavor) {
    this.gameService.vote(this.game.id, this.player.id, inFavor).subscribe((response) => {
      console.log(response);
    });
  }

  public choose() {
    this.gameService.choose(this.game.id, this.player.id, this.choiceNumber).subscribe((response) => {
      console.log(response);
    });
  }
}
