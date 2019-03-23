export const joinServer = (body, joinKey) => {
	return {
		type: 'JOIN_SERVER',
		fetch: {
			type: 'POST',
			url: 'Game/Join/' + joinKey
		},
		payload: body
	}
}

export const startServer = id => {
	return {
		type: 'START_SERVER',
		fetch: {
			type: 'POST',
			url: 'Game/Start/' + id
		},
		payload: null
	}
}

export const createServer = () => {
	return {
		type: 'CREATE_SERVER',
		fetch: {
			type: 'POST',
			url: 'Game'
		},
		payload: null
	}
}

//Test httpGET. Used for testing (to send all game info to all connected players)
export const gameInfo = gameID => {
	return {
		type: 'GET_GAME_INFO',
		fetch: {
			type: 'GET',
			url: 'Game/' + gameID
		}
	}
}

//incoming socket dispatches this action.
export const updateGameFromSocket = payload => {
	return {
		type: 'UPDATE_GAME',
		payload: payload
	}
}

export const choosePlayer = (gameId, chosenPlayerId) => {
	return {
		type: 'CHOOSE_PLAYER',
		fetch: {
			type: 'POST',
			url: 'Game/' + gameId + '/Choose/' + chosenPlayerId,
			headers: {
				'X-Player' : true
			}
		},
		payload: null
	}
}
