import {Game} from "./game";

export interface ChoiceRound {
    id: number;
    gameId: number;
    game: Game;
    dateCreated: string;
}