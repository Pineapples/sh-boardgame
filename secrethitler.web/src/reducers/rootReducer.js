/*
 src/reducers/rootReducer.js
*/

import { combineReducers } from 'redux';
import serverReducer from './serverReducer';
import playerReducer from './playerReducer';

export default combineReducers({
 server : serverReducer,
 player : playerReducer
});
