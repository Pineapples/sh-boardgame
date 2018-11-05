import {Policy} from "./policy";
import {VoteRound} from "./voteRound";
import {Player} from "./player";
import {ChoiceRound} from "./choiceRound";
import {GameState} from "./gameState";

export class Game {
    id: number;
    name: string;
    dateCreated: string;
    gameStateId: GameState;
    voteRounds: VoteRound[];
    choiceRounds: ChoiceRound[];
    players: Player[];
    policies: Policy[];
    joinKey: string;
}