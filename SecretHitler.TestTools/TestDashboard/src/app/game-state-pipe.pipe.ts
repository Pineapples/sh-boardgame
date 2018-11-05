import { Pipe, PipeTransform } from '@angular/core';
import {GameState} from './models/gameState';

@Pipe({
  name: 'gameState'
})
export class GameStatePipe implements PipeTransform {

  transform(value: any): any {
    return GameState[value];
  }

}
