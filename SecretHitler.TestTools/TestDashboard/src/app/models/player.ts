import {Game} from "./game";

export interface Player {
    id: number;
    userName: string;
    gameId: number;
    game: Game;
    role: number;
}