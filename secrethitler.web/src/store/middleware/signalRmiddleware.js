import { joinGame } from './signalRconnection.js';

export const signalRmiddleware = store => next => action => {
	if(action.type === 'JOIN_SERVER'){
		joinGame(action.payload.id)
	}
	next(action)
}
