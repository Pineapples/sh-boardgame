export default (state = {idScreenOpen:false}, action) => {
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
		case 'TOGGLE_ID_SCREEN':
			return {
				...state,
				idScreenOpen: !state.idScreenOpen
			}
		  default:
			   return state
	}
}
