import {Game} from "./game";

export interface VoteRound {
    id: number;
    gameId: number;
    game: Game;
    dateCreated: string;
}