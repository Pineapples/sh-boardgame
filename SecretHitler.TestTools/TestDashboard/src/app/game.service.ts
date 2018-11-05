import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Game} from './models/game';
import {Observable} from 'rxjs';
import {Player} from './models/player';

@Injectable()
export class GameService {

    private readonly baseUrl = "http://localhost:5000/api";

    constructor(private httpClient: HttpClient) {
    }

    public getGames(): Observable<Game[]> {
        return this.httpClient.get<Game[]>(`${this.baseUrl}/Game`);
    }

    public createGame(): Observable<Game> {
        return this.httpClient.post<Game>(`${this.baseUrl}/Game`, null);
    }

    public getGame(gameId: number): Observable<Game> {
        return this.httpClient.get<Game>(`${this.baseUrl}/Game/${gameId}`);
    }

    public startGame(gameId): Observable<Game> {
        return this.httpClient.post<Game>(`${this.baseUrl}/Game/Start/${gameId}`, null);
    }

    public joinGame(userName: string, joinKey: string): Observable<Player> {
        return this.httpClient.post<Player>(`${this.baseUrl}/Game/Join/${joinKey}`, { userName: userName });
    }

    public vote(gameId: number, playerId: number, inFavor: boolean) {
        return this.httpClient.post<Game>(`${this.baseUrl}/Game/${gameId}/Vote/${inFavor}`, null, { headers: new HttpHeaders({
                "X-Player": playerId.toString()
            })
        });
    }

    public choose(gameId: number, playerId: number, chosenPlayer: number) {
        return this.httpClient.post<Game>(`${this.baseUrl}/Game/${gameId}/Choose/${chosenPlayer}`, null, { headers: new HttpHeaders({
                "X-Player": playerId.toString()
            })
        });
    }
}
