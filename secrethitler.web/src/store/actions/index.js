/*
 src/actions/simpleAction.js
*/
export const joinServer = playerData => {
	return {
		type: 'JOIN_SERVER',
		payload: playerData
	}
}

export const toggleIDScreen = () => {
	return {
		type: 'TOGGLE_ID_SCREEN'
	}
}

export const createServer = serverData => {
	return {
		type: 'CREATE_SERVER',
		payload: serverData
	}
}
