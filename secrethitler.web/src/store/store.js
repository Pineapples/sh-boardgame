/*
 * src/store.js
*/

import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './reducers';

//initstate empty object
export default function configureStore(initialState={}) {
	return createStore(
		rootReducer,
		applyMiddleware(thunk)
	);
}
