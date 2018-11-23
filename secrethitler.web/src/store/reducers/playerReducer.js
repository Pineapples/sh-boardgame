export default (state = {idScreenOpen:true}, action) => {
	switch (action.type) {
		case 'JOIN_SERVER':
		   return {
				...action.payload,
				idScreenOpen: state.idScreenOpen
		   }
		   case 'TOGGLE_ID_SCREEN':
				   // console.log('Toggling screens', state.idScreenOpen, !state.idScreenOpen)
				return {
					...state,
					idScreenOpen: !state.idScreenOpen
				}
		  default:
			   return state
	}
}
