import {Policy} from "./policy";
import {VoteRound} from "./voteRound";
import {Player} from "./player";
import {ChoiceRound} from "./choiceRound";

export interface Game {
    id: number;
    name: string;
    dateCreated: string;
    gameStateId: number;
    voteRounds: VoteRound[];
    choiceRounds: ChoiceRound[];
    players: Player[];
    policies: Policy[];
    joinKey: string;
}