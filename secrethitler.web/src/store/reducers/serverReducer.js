/*
 src/reducers/simpleReducer.js
*/

export default (state = {}, action) => {
 switch (action.type) {
  case 'CREATE_SERVER':
   return {
	...action.payload
   }
  default:
   return state
 }
}
