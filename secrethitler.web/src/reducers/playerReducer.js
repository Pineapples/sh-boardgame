/*
 src/reducers/simpleReducer.js
*/

export default (state = {}, action) => {
 switch (action.type) {
  case 'JOIN_SERVER':
   return {
    playerData: action.payload
   }
  default:
   return state
 }
}
