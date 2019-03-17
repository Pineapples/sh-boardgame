import { joinGame } from './signalRconnection.js';

export const signalRMiddleware = store => next => action => {
	if(action.type === 'JOIN_SERVER'){
		console.log('signalRmidware here', action);

		console.log('invoking?', action.payload.id)
		joinGame(action.payload.id)
	}
	next(action)
}
