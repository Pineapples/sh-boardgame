/*
 * src/store.js
*/

import { createStore, applyMiddleware } from 'redux';
import { persistStore, persistReducer } from 'redux-persist';
import storageSession from 'redux-persist/lib/storage/session'
import autoMergeLevel2 from 'redux-persist/lib/stateReconciler/autoMergeLevel2';

import thunk from 'redux-thunk';
import rootReducer from './reducers';

const persistConfig = {
 key: 'root',
 storage: storageSession,
 stateReconciler: autoMergeLevel2 // see "Merge Process" section for details.
};

const pReducer = persistReducer(persistConfig, rootReducer);

//initstate empty object
// export function configureStore(initialState={}) {
// 	return createStore(
// 		pReducer,
// 		applyMiddleware(thunk)
// 	);
// }
export const configureStore = createStore(pReducer, applyMiddleware(thunk));
export const persistor = persistStore(configureStore);
