import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Game} from './models/game';
import {Observable} from 'rxjs';

@Injectable()
export class GameService {

    private readonly baseUrl = "localhost:5000/api";

    constructor(private httpClient: HttpClient) {
    }

    public getGames(): Observable<Game[]> {
        return this.httpClient.get<Game[]>(`${this.baseUrl}/Game`);
    }

    public createGame(): Observable<Game> {
        return this.httpClient.post<Game>(`${this.baseUrl}/Game`, null);
    }
}
