/*
 src/actions/simpleAction.js
*/

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

export const toggleIDScreen = () => {
	return {
		type: 'TOGGLE_ID_SCREEN'
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

export const gameInfo = gameID => {
	return {
		type: 'GET_GAME_INFO',
		fetch: {
			type: 'GET',
			url: 'Game/' + gameID
		}
	}
}
