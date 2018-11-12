/*
 src/actions/simpleAction.js
*/
export const joinServer = playerData => {
	return {
		type: 'JOIN_SERVER',
		payload: playerData
	}
}

export const createServer = serverData => {
	return {
		type: 'CREATE_SERVER',
		payload: serverData
	}
}
