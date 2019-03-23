export default (state = {}, action) => {
	switch (action.type) {
		case 'JOIN_SERVER':
			return {
				...action.payload,
				idScreenOpen: state.idScreenOpen
			}
		case 'UPDATE_GAME':
			console.log('actiepaylading', action.payload)
			return {
				...state,
				game: action.payload
			}
			//This only updates the screen to stop showing the options.
			//Other cases like vote can be added.
		case 'CHOOSE_PLAYER':
			return{
				...state,
				game: {
					...state.game,
					gameStateId: 2
				}
			}
		  default:
			   return state
	}
}
